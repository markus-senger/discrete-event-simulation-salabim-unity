import salabim as sim
from unitysalabim import *
import json
import sys

env = sim.Environment(trace=False)
env.background_color('20%gray')
env.speed(int(sys.argv[1]))

with open('C:/Users/Senge/OneDrive/Desktop/discrete-event-simulation-salabim-unity/unity/simulation.json', 'r') as file:
    json_data = json.load(file)

conveyor_dtos = json_data["conveyorDTOs"]
connection_point_dtos = json_data["connectionPointDTOs"]
wt_dtos = json_data["WTDTOs"]
spawner_dtos = json_data["SpawnerDTOs"]
removal_dtos = json_data["RemovalDTOs"]
wait_dtos = json_data["WaitDTOs"]

conveyor_dict = {}
for conveyor_id, conveyor_data in conveyor_dtos.items():    
    if conveyor_data['rotation'] == 0:
        conveyor_dict[conveyor_id] = Conveyor(x1=conveyor_data['x'] - 36, y1=conveyor_data['y'], x2=conveyor_data['x'] + 36, y2=conveyor_data['y'])
    elif conveyor_data['rotation'] == 180:
        conveyor_dict[conveyor_id] = Conveyor(x1=conveyor_data['x'] + 36, y1=conveyor_data['y'], x2=conveyor_data['x'] - 36, y2=conveyor_data['y'])
    elif conveyor_data['rotation'] == 90:
        conveyor_dict[conveyor_id] = Conveyor(x1=conveyor_data['x'], y1=conveyor_data['y'] - 36, x2=conveyor_data['x'], y2=conveyor_data['y'] + 36)
    else:
        conveyor_dict[conveyor_id] = Conveyor(x1=conveyor_data['x'], y1=conveyor_data['y'] + 36, x2=conveyor_data['x'], y2=conveyor_data['y']- 36)

connection_point_dict = {}
for connection_point_id, connection_point_data in connection_point_dtos.items():
    connection_point_dict[conveyor_dict[connection_point_data['conveyor2Id']]] = ConnectionPoint(x=connection_point_data['x'], y=connection_point_data['y'], 
                                                                 c1=conveyor_dict[connection_point_data['conveyor2Id']], c2=conveyor_dict[connection_point_data['conveyor1Id']])

ConnectionPointHandler.setup(connection_point_dict)    

for wait_id, wait_data in wait_dtos.items():  
    conveyor_dict[wait_data['conveyorId']].setWaitPos(True, wait_data['waitDuration'])

for spawner_id, spawner_data in spawner_dtos.items():  
    conveyor_dict[spawner_data['conveyorId']].setSpawnerPos(True, spawner_data['spawnFrequency'])

outputConveyor = None
for removal_id, removal_data in removal_dtos.items():  
    outputConveyor = conveyor_dict[removal_data['conveyorId']]
    outputConveyor.setRemovalPos(True, removal_data['removalDuration'])




wt_dict = {}
for wt_id, wt_data in wt_dtos.items():
    wt_dict[wt_id] = Carrier(con=conveyor_dict[wt_data['conveyorId']], dist=50)

env.animate(True)
env.run(int(sys.argv[2]))

print(outputConveyor.removalValue)
sys.exit(outputConveyor.removalValue)

