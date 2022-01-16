from typing import cast
pt = tuple[int, int]

a: list[list[int]] = []
folds: list[tuple[str, int]] = []
with open('input/day13.txt') as f:
    for line in f:
        if line == '\n':
            continue
        elif 'fold' not in line:
            a.append([int(k) for k in line.strip().split(',')])
        else:
            s = line.strip().split('=')
            folds.append(cast(tuple[str, int], tuple([s[0][-1], int(s[1])])))

def fold(a: list[list[int]], f: tuple[str, int]):
    if f[0] == 'x':
        x = f[1]
        for i in range(len(a)):
            xi = a[i][0]
            if xi > x:
                d = (xi - x) * 2 
                xi -= d
                a[i][0] = xi
    elif f[0] == 'y':
        y = f[1]
        for i in range(len(a)):
            yi = a[i][1]
            if yi > y:
                d = (yi - y) * 2 
                yi -= d
                a[i][1] = yi

# Part One
fold(a, folds[0])
points: set[pt] = set()
for pr in a:
    points.add(cast(pt, tuple(pr)))
print(len(points))

# Part Two
for i in range(1, len(folds)):
    fold(a, folds[i])
points = set()
for pr in a:
    points.add(cast(pt, tuple(pr)))

# grid parameters are from inspecting the output
# answer is the sequence of '#' ascii letters drawn 
grid: list[list[str]] = [['.']*39 for i in range(6)]
for pr in points:
    grid[pr[1]][pr[0]] = '#'
print(*grid, sep='\n')
