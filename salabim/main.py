import salabim as sim
from unitysalabim import *

env = sim.Environment(trace=False)
env.background_color('20%gray')


c1 = Conveyor(x1=100, y1=100, x2=100, y2=300)
c2 = Conveyor(x1=100, y1=300, x2=100, y2=500)
c3 = Conveyor(x1=100, y1=500, x2=300, y2=500)
c4 = Conveyor(x1=300, y1=500, x2=500, y2=500)
c5 = Conveyor(x1=500, y1=500, x2=500, y2=700)
c6 = Conveyor(x1=500, y1=700, x2=700, y2=700)
c7 = Conveyor(x1=700, y1=700, x2=700, y2=500)
c8 = Conveyor(x1=700, y1=500, x2=700, y2=300)
c9 = Conveyor(x1=700, y1=300, x2=900, y2=300)
c10 = Conveyor(x1=900, y1=300, x2=900, y2=100)
c11 = Conveyor(x1=900, y1=100, x2=700, y2=100)
c12 = Conveyor(x1=700, y1=100, x2=500, y2=100)
c13 = Conveyor(x1=500, y1=100, x2=500, y2=300)
c14 = Conveyor(x1=500, y1=300, x2=300, y2=300)
c15 = Conveyor(x1=300, y1=300, x2=300, y2=100)
c16 = Conveyor(x1=300, y1=100, x2=100, y2=100)

ConnectionPointHandler.setup({
    c1: ConnectionPoint(x=100, y=300, c1=c1, c2=c2),
    c2: ConnectionPoint(x=100, y=500, c1=c2, c2=c3),
    c3: ConnectionPoint(x=300, y=500, c1=c3, c2=c4),
    c4: ConnectionPoint(x=500, y=500, c1=c4, c2=c5),
    c5: ConnectionPoint(x=500, y=700, c1=c5, c2=c6),
    c6: ConnectionPoint(x=700, y=700, c1=c6, c2=c7),
    c7: ConnectionPoint(x=700, y=500, c1=c7, c2=c8),
    c8: ConnectionPoint(x=700, y=300, c1=c8, c2=c9),
    c9: ConnectionPoint(x=900, y=300, c1=c9, c2=c10),
    c10: ConnectionPoint(x=900, y=100, c1=c10, c2=c11),
    c11: ConnectionPoint(x=700, y=100, c1=c11, c2=c12),
    c12: ConnectionPoint(x=500, y=100, c1=c12, c2=c13),
    c13: ConnectionPoint(x=500, y=300, c1=c13, c2=c14),
    c14: ConnectionPoint(x=300, y=300, c1=c14, c2=c15),
    c15: ConnectionPoint(x=300, y=100, c1=c15, c2=c16),
    c16: ConnectionPoint(x=100, y=100, c1=c16, c2=c1),
})

Carrier(con=c1)
Carrier(con=c2)
Carrier(con=c2, dist=70)
Carrier(con=c3)
Carrier(con=c4)
Carrier(con=c6, dist=50)
Carrier(con=c9, dist=60)
Carrier(con=c11, dist=70)
Carrier(con=c12)
Carrier(con=c13)
Carrier(con=c15)

env.animate(True)
env.run(env.inf)