import salabim as sim
from .utils import *
from .conveyor import *
from .connection_point_handler import *
import random
import time

class Carrier(sim.Component):
    def setup(self, con: Conveyor, dist: float = 0):
        self.conveyor = con
        self.x = con.x1 + abs(con.x1 - con.x2) * ((dist * (-1 if con.x1 - con.x2 > 0 else 1)) / 100)
        self.y = con.y1 + abs(con.y1 - con.y2) * ((dist * (-1 if con.y1 - con.y2 > 0 else 1)) / 100)

        random.seed(int(time.time()) + self.x + self.y)
        self.rec = sim.AnimateRectangle(
            spec = lambda: [self.x - 12, self.y - 12, self.x + 12, self.y + 12],
            fillcolor = "green" #colors[random.randint(0, len(colors) - 1)]
        )

    def process(self):
        while True:
            move = False
            if self.y != self.conveyor.y2:
                self.y = self.y + (self.conveyor.y2 - self.conveyor.y1) / 10
                move = True
            if self.x != self.conveyor.x2:
                self.x = self.x + (self.conveyor.x2 - self.conveyor.x1) / 10
                move = True
            if not move:
                c = ConnectionPointHandler.get(self.conveyor)
                self.conveyor = c.c2
            else:
                self.hold(1)