import salabim as sim
from .utils import *
from .conveyor import *
from .connection_point_handler import *
import random
import time

class Carrier(sim.Component):
    def setup(self, con: Conveyor, dist: float = 0):
        self.conveyor = con
        self.conveyor.isFree = False
        self.x = con.x1 + abs(con.x1 - con.x2) * ((dist * (-1 if con.x1 - con.x2 > 0 else 1)) / 100)
        self.y = con.y1 + abs(con.y1 - con.y2) * ((dist * (-1 if con.y1 - con.y2 > 0 else 1)) / 100)
        self.curTimeOnConveyor = 0
        self.hasProduct = False

        random.seed(int(time.time()) + self.x + self.y)
        self.rec = sim.AnimateRectangle(
            spec = lambda: [self.x - 12, self.y - 12, self.x + 12, self.y + 12],
            fillcolor = "green" #colors[random.randint(0, len(colors) - 1)]
        )

        self.product = sim.AnimateRectangle(
            spec = lambda: [self.x, self.y, self.x, self.y ],
        )

    def process(self):
        while True:
            move = False

            if self.curTimeOnConveyor == 10/2 and (self.conveyor.wait or self.conveyor.spawn or self.conveyor.removal):
                self.hold(self.conveyor.waitTime)

                if self.conveyor.spawn:
                    self.product.fillcolor = fillcolor = colors[random.randint(0, len(colors) - 1)]
                    self.product.spec = lambda: [self.x - 5, self.y - 5, self.x + 5, self.y + 5]
                    self.hasProduct = True
                
                if self.conveyor.removal and self.hasProduct:
                    self.product.spec = lambda: [self.x, self.y, self.x, self.y]
                    self.conveyor.removalValue = self.conveyor.removalValue + 1
                    self.hasProduct = False

            if round(self.y, 1) != round(self.conveyor.y2, 1):
                self.y = self.y + (self.conveyor.y2 - self.conveyor.y1) / 10
                move = True
            if round(self.x, 1) != round(self.conveyor.x2, 1):
                self.x = self.x + (self.conveyor.x2 - self.conveyor.x1) / 10
                move = True

            if not move:
                oldConveyor = self.conveyor
                c = ConnectionPointHandler.get(self.conveyor)
                self.conveyor = c.c2
                self.curTimeOnConveyor = 0

                while not self.conveyor.isFree:
                    self.hold(0.01)

                oldConveyor.isFree = True
                self.conveyor.isFree = False
            else:
                self.hold(1)
                self.curTimeOnConveyor = self.curTimeOnConveyor + 1