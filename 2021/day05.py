from typing import cast
pt = tuple[int, int]

def walk(visit: dict[pt, int], a: pt, b: pt, diagonal: bool=False) -> None:
    """Visits each coordinate traversed from points a to b"""
    x1, y1, x2, y2 = a[0], a[1], b[0], b[1]
    if x1 == x2:
        for yi in range(min(y1, y2), max(y1, y2) + 1):
            if (x1, yi) in visit:
                visit[(x1, yi)] += 1
            else:
                visit[(x1, yi)] = 1
    elif y1 == y2:
        for xi in range(min(x1, x2), max(x1, x2) + 1):
            if (xi, y1) in visit:
                visit[(xi, y1)] += 1
            else:
                visit[(xi, y1)] = 1
    elif diagonal == True:
        # case work
        if x1 < x2 and y1 < y2:
            while (x1, y1) != (x2 + 1, y2 + 1):
                if (x1, y1) in visit:
                    visit[(x1, y1)] += 1
                else:
                    visit[(x1, y1)] = 1
                x1 += 1
                y1 += 1
        elif x1 < x2 and y1 > y2:
            while (x1, y1) != (x2 + 1, y2 - 1):
                if (x1, y1) in visit:
                    visit[(x1, y1)] += 1
                else:
                    visit[(x1, y1)] = 1
                x1 += 1
                y1 -= 1
        elif x1 > x2 and y1 < y2:
            while (x1, y1) != (x2 - 1, y2 + 1):
                if (x1, y1) in visit:
                    visit[(x1, y1)] += 1
                else:
                    visit[(x1, y1)] = 1
                x1 -= 1
                y1 += 1
        elif x1 > x2 and y1 > y2:
            while (x1, y1) != (x2 - 1, y2 - 1):
                if (x1, y1) in visit:
                    visit[(x1, y1)] += 1
                else:
                    visit[(x1, y1)] = 1
                x1 -= 1
                y1 -= 1

a: list[pt]  = []
b: list[pt]  = []
with open('input/day5.txt') as f:
    for line in f:
        m, n = line.strip().split(' -> ')
        a.append(cast(pt, ([int(k) for k in m.split(',')])))
        b.append(cast(pt, ([int(k) for k in n.split(',')])))

# Part One
visit: dict[pt, int] = {}
for i in range(len(a)):
    walk(visit, a[i], b[i])
cnt = 0
for k, v in visit.items():
    if v > 1:
        cnt += 1
print(cnt)

# Part Two
visit = {}
for i in range(len(a)):
    walk(visit, a[i], b[i], diagonal=True)
cnt = 0
for k, v in visit.items():
    if v > 1:
        cnt += 1
print(cnt)
