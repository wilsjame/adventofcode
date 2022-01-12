pt = tuple[int, int]
grid = list[list[int]]

a: list[list[int]] = []
with open('input/day11.txt') as f:
    for line in f:
        a.append([int(x) for x in line.strip()])

def ok(u: pt) -> bool:
    """return true if pt is inbounds"""
    if u[0] >= 0 and u[0] < 10 and u[1] >= 0 and u[1] < 10:
        return True
    else:
        return False

def dfs(s: pt, a: grid) -> None:
    """depth first search flashing nodes"""
    i: int = s[0]
    j: int = s[1]
    dx: list[int] = [1, 1, 0, -1, -1, -1, 0, 1]  
    dy: list[int] = [0, -1, -1, -1, 0, 1, 1, 1]  

    if a[i][j] > 9:
        a[i][j] = 0
        for k in range(8):
            if ok((i + dx[k], j + dy[k])) and a[i + dx[k]][j + dy[k]] > 0:
                a[i + dx[k]][j + dy[k]] += 1
                if a[i + dx[k]][j + dy[k]] > 9:
                    dfs((i + dx[k], j + dy[k]), a)

# Part One and Two
INF = 250
cnt = 0
for steps in range(INF):
    for i in range(10):
        for j in range(10):
            a[i][j] += 1
    for i in range(10):
        for j in range(10):
            dfs((i, j), a)
    cnt += sum(row.count(0) for row in a)
    if steps == 100 - 1:
        print(cnt)
    if sum(row.count(0) for row in a) == 100:
        print(steps + 1)
        break
