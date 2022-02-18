from collections import defaultdict
from typing import DefaultDict

Range = list[int, int]
Step = list[str, Range, Range, Range] 
steps: list[Step] = []

with open('input/day22.txt') as f:
    while line := f.readline():
        step: Step = []
        step.append(line.split()[0])
        for ranges in line.strip().split('=')[1:]:
            step.append([int(k) for k in ranges.strip(',yz').split('..')])
        steps.append(step)

def inrange(x: Range, y: Range, z: Range) -> bool:
    if x[0] < -50 or y[0] < -50 or y[0] < -50 or x[1] > 50 or x[1] > 50 or z[1] > 50:
        return False
    return True

Pt = tuple[int, int, int]
vol: DefaultDict[Pt, bool] = defaultdict(bool)

def fill(step: Step) -> None:
    pwr: str = step[0]
    x: Range = step[1]
    y: Range = step[2]
    z: Range = step[3]
    if inrange(x, y, z):
        for i in range(x[0], x[1]+1):
            for j in range(y[0], y[1]+1):
                for k in range(z[0], z[1]+1):
                    vol[(i, j, k)] = pwr == 'on'

for step in steps:
    fill(step)

cnt: int = 0
for k,v in vol.items():
    cnt += v == True
print(cnt)
