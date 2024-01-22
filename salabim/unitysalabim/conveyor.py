import salabim as sim

class Conveyor(sim.Component):
    def setup(self, x1, y1, x2, y2):
        self.x1 = x1
        self.y1 = y1
        self.x2 = x2
        self.y2 = y2
        sim.AnimateLine(spec = (x1, y1, x2, y2), linewidth=30)