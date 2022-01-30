from ast import literal_eval
from collections import Counter, defaultdict
Dist = tuple[int, int, int]
Coord = tuple[int, int, int]
Scan = list[list[int]]
scans: list[Scan] = []
with open('input/day19.txt') as f:
    ll: list[str] = [x for x in f.read().strip().split('\n\n')]
    scans = [[literal_eval('[' + x + ']') for x in l.split('\n')[1:]] for l in ll]
    
axes_rotate: list[tuple[int, int, int]] = [(0, 1, 2), (0, 2, 1), (1, 0, 2), (1, 2, 0), (2, 0, 1), (2, 1, 0)]
axes_negation: list[tuple[int, int, int]] = [(1, 1, 1), (1, -1, 1), (1, 1, -1), (1, -1, -1), (-1, 1, 1), (-1, -1, 1), (-1, 1, -1), (-1, -1, -1)]

def apply_relative(s: Scan, rotate: Coord, negat: Coord, rel: Dist) -> Scan:
    """apply orientations and relative positioning to scan s"""
    res: Scan = []
    for coord in s:
        res.append([rel[0] - negat[0]*coord[rotate[0]], rel[1] - negat[1]*coord[rotate[1]], rel[2] - negat[2]*coord[rotate[2]]])
    return res

def find_align(a: Scan, b: Scan) -> (bool, Dist, list[Coord], Scan):
    """find alignment of b relative to a and return shared beacons"""
    for rotate in axes_rotate:
        for negat in axes_negation:
            from_a: Counter[Dist] = Counter()
            beacons: defaultdict[Dist, list[Coord]] = defaultdict(list)
            for coord_a in a:
                for coord_b in b:
                    dist: Dist = (negat[0]*coord_b[rotate[0]] + coord_a[0], negat[1]*coord_b[rotate[1]] + coord_a[1], negat[2]*coord_b[rotate[2]] + coord_a[2])
                    from_a[dist] += 1
                    beacons[dist].append((coord_a[0], coord_a[1], coord_a[2]))
            for rel, cnt in from_a.items():
                if cnt >= 12:
                    b_rel_a = apply_relative(b, rotate, negat, rel)
                    return True, rel, beacons[rel], b_rel_a
    return False, None, None, None

# Part One
aligned_indices: set[int] = set()
aligned_indices.add(0)
aligned_scanners: set[Coord] = set()
aligned_scanners.add((0, 0, 0))
aligned_beacons: set[Coord] = set()
all_aligned: bool = False
while all_aligned == False:
    all_aligned = True
    for i in range(len(scans)):
        for j in range(len(scans)):
            if i in aligned_indices:
                ok, rel_dist, beacons, j_rel_i = find_align(scans[i], scans[j])
                if ok: 
                    if rel_dist != (0, 0, 0):
                        all_aligned = False
                    aligned_indices.add(j)
                    aligned_scanners.add(rel_dist)
                    [aligned_beacons.add(x) for x in beacons]
                    scans[j] = j_rel_i
print(len(aligned_beacons))

# Part Two
def manhattan_dist(a: Coord, b: Coord) -> int:
    res: int = 0
    [res := res + abs(a[i] - b[i]) for i in range(3)]
    return res

mx = 0
aligned_scanners = list(aligned_scanners)
for i in range(len(aligned_scanners)):
    for j in range(len(aligned_scanners)):
        mx = max(mx, manhattan_dist(aligned_scanners[i], aligned_scanners[j]))
print(mx)
