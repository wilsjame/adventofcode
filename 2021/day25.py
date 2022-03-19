from copy import deepcopy
Board = list[list[str]]
b: Board = []
with open('input/day25.txt') as f:
    for l in f:
        b.append([x for x in l.strip()])
mxI: int = len(b)
mxJ: int = len(b[0])
def step(b: Board) -> Board:
    b2: Board = deepcopy(b)
    # East
    for i in range(mxI):
        for j in range(mxJ):
            if b[i][j] == '>' and b[i][(j+1)%mxJ] == '.':
                b2[i][j] = '.'
                b2[i][(j+1)%mxJ] = '>'
    b = deepcopy(b2)
    # South
    for i in range(mxI):
        for j in range(mxJ):
            if b[i][j] == 'v' and b[(i+1)%mxI][j] == '.':
                b2[i][j] = '.'
                b2[(i+1)%mxI][j] = 'v'
    return b2
cnt: int = 0
while True:
    cnt +=1
    prev = deepcopy(b)
    b = step(b)
    if b == prev:
        break
print(cnt)
