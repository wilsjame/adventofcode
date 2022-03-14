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


# Part One

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

# Part Two coordinate compression

# this approach uses a ton of memory >10GB rip
# crashes cpython, atleast on my pc
# works using pypy, but will crash, sometimes ;_;

# store unique x, y, and z coordinates
# map coordinates to compressed indices
# map compressed indices to coordinates
# store compressed 'on' cubes
# expand compressed 'on' cubes

X: set[int] = set()
Y: set[int] = set()
Z: set[int] = set()
for s in steps:
    X.update([s[1][0], s[1][1] + 1]) # add one to include upper bound
    Y.update([s[2][0], s[2][1] + 1]) # ex) 1..2 is two elements 
    Z.update([s[3][0], s[3][1] + 1]) #     (2 + 1) - 1 = 2
X = sorted(X)
Y = sorted(Y)
Z = sorted(Z)

Xx: dict[int, int] = {}
Yy: dict[int, int] = {}
Zz: dict[int, int] = {}
for i, v in enumerate(X):
    Xx[v] = i
for i, v in enumerate(Y):
    Yy[v] = i
for i, v in enumerate(Z):
    Zz[v] = i
xX: dict[int, int] = {}
yY: dict[int, int] = {}
zZ: dict[int, int] = {}
for k, v in Xx.items():
    xX[v] = k
for k, v in Yy.items():
    yY[v] = k
for k, v in Zz.items():
    zZ[v] = k

V: set[Pt] = set() # compressed 'on' cubes
for i, s in enumerate(steps):
    #print(i+1, len(steps))
    x0: int = Xx[s[1][0]] 
    x1: int = Xx[s[1][1] + 1] 
    y0: int = Yy[s[2][0]]
    y1: int = Yy[s[2][1] + 1]
    z0: int = Zz[s[3][0]]
    z1: int = Zz[s[3][1] + 1]
    for i in range(x0, x1):
        for j in range(y0, y1):
            for k in range(z0, z1):
                if s[0] == 'on':
                    V.add((i, j, k))
                else:
                    V.discard((i, j, k))

ans = 0
for i, j, k in V:
    ans += (xX[i+1] - xX[i]) * (yY[j+1] - yY[j]) * (zZ[k+1] - zZ[k])
print(ans)
