import salabim as sim
from .conveyor import *

class ConnectionPoint(sim.Component):
    def setup(self, x, y, c1: Conveyor, c2: Conveyor):
        sim.AnimateRectangle(spec = (x-15, y-15, x+15, y+15), fillcolor="gray")
        self.c1 = c1
        self.c2 = c2