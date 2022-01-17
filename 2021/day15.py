import heapq
import math
pt = tuple[int, int]

a: list[list[int]] = []
with open('input/day15.txt') as f:
    for r in f:
        a.append([int(x) for x in r.strip()])

def dijkstra(start: pt, processed: dict[int, bool], distance: dict[pt, int], g: dict[pt, int]) -> None:
    """returns when distance has all shortest paths from start to every node in g"""
    pq = []
    distance[start] = 0
    heapq.heappush(pq, (0, start))
    while len(pq) > 0:
        cur = heapq.heappop(pq)
        ci = cur[1][0]
        cj = cur[1][1]
        if cur[1] in processed: # smol speed ^
            continue
        processed[cur[1]] = True
        # neighbors
        dx: list[int] = [0, 1, 0, -1]
        dy: list[int] = [1, 0, -1, 0]
        for k in range(4):
            ni = ci + dx[k]
            nj = cj + dy[k]
            if (ni, nj) in g and (ni, nj) not in processed:
                dn = g[(ni, nj)]
                if (ni, nj) in distance:
                    distance[(ni, nj)] = min(distance[(ni, nj)], distance[(ci, cj)] + dn)
                else:
                    distance[(ni, nj)] = distance[(ci, cj)] + dn
                heapq.heappush(pq, (distance[(ni, nj)], (ni, nj)))

# Part One
d: dict[pt, int] = {}
distance: dict[pt, int] = {} 
processed: dict[int, bool] = {}
N = len(a)
for i in range(N):
    for j in range(N):
        d[(i, j)] = a[i][j]

dijkstra((0, 0), processed, distance, d)
mxN = int(math.sqrt(len(d)))
print(distance[(mxN - 1, mxN - 1)])

# Part Two
add = [[0, 1, 2, 3, 4], 
       [1, 2, 3, 4, 5],
       [2, 3, 4, 5, 6],
       [3, 4, 5, 6, 7],
       [4, 5, 6, 7, 8]]
            
# expand d 5x original size and add scaling
for I in range(5):
    for J in range(5):
        di = I * N
        dj = J * N
        for i in range(N):
            for j in range(N):
                val = (a[i][j] + add[I][J])
                if val > 9:
                    val %= 9
                d[(di+i, dj+j)] = val 

distance = {} 
processed = {}
dijkstra((0, 0), processed, distance, d)
mxN = int(math.sqrt(len(d)))
print(distance[(mxN - 1, mxN - 1)])
