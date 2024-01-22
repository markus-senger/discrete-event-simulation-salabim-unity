import salabim as sim
from .conveyor import *

class ConnectionPointHandler(sim.Component):
    connectionPoints = {}

    @staticmethod
    def setup(connectionPoints: dict):
        ConnectionPointHandler.connectionPoints = connectionPoints

    @staticmethod
    def get(c: Conveyor):
        return ConnectionPointHandler.connectionPoints.get(c)