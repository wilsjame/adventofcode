from collections import deque

a: list[list[int]] = []
with open('input/day9.txt') as f:
    for line in f:
        a.append([int(x) for x in line.strip()])

def is_low(i: int, j: int, a: list[list[int]]) -> bool:
    """Determines if the point (i, j) is a low point."""
    dx = [0, 1, 0, -1]
    dy = [1, 0, -1, 0]

    for k in range(4):
        if i + dx[k] >= 0 and i + dx[k] < len(a) and j + dy[k] >= 0 and j + dy[k] < len(a[0]):
                if a[i][j] >= a[i + dx[k]][j + dy[k]]:
                    return False
    return True

# Part One
n = len(a)
m = len(a[0])
res = 0
for i in range(n):
    for j in range(m):
        if is_low(i, j, a):
            res += (a[i][j] + 1)
print(res)

# Part Two
pt = tuple[int, int]

def bfs(start: pt, a: list[list[int]]) -> int:
    """flood fill nodes with height greater than the current node."""
    visited: dict[pt, bool] = {}
    q: deque[pt] = deque([start])
    while len(q) > 0:
        cur = q.pop()
        visited[cur] = True
        i = cur[0]
        j = cur[1]
        dx = [0, 1, 0, -1]
        dy = [1, 0, -1, 0]
        for k in range(4):
            if i + dx[k] >= 0 and i + dx[k] < len(a) and j + dy[k] >= 0 and j + dy[k] < len(a[0]):
                adj_pos = (i + dx[k], j + dy[k]) 
                adj_val = a[i + dx[k]][j + dy[k]]
                if adj_val > a[i][j] and adj_val < 9 and adj_pos not in visited:
                    q.append(adj_pos)
    return len(visited)

basins: list[int] = [] # priority queue would also work
for i in range(n):
    for j in range(m):
        if is_low(i, j, a):
            basins.append(bfs((i, j), a))
basins.sort(reverse=True)
print(basins[0] * basins[1] * basins[2])
