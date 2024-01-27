import salabim as sim
from .utils import *

class Conveyor(sim.Component):
    def setup(self, x1, y1, x2, y2):
        self.x1 = x1
        self.y1 = y1
        self.x2 = x2
        self.y2 = y2
        self.isFree = True

        self.wait = False
        self.spawn = False
        self.removal = False
        self.waitTime = 0

        self.line = sim.AnimateLine(spec = (x1, y1, x2, y2), linewidth=30)
        self.removalTextField = None
        self.removalValue = 0

    def setWaitPos(self, value: bool, waitTime: float):
        self.wait = value
        self.waitTime = waitTime
        self.line.linecolor = wait_pos_color

    def setSpawnerPos(self, value: bool, spawnTime: float):
        self.spawn = value
        self.waitTime = spawnTime
        self.line.linecolor = spawn_pos_color

    def setRemovalPos(self, value: bool, removalTime: float):
        self.removal = value
        self.waitTime = removalTime
        self.line.linecolor = removal_pos_color
        
        self.removalTextField = sim.AnimateText(text=lambda: str(self.removalValue), x=(self.x1 + self.x2) / 2 - 10, y=(self.y1 - 32))
