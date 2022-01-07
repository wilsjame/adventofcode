from typing import Tuple, Mapping
array2D = Tuple[Tuple[int]]

class Board:
    """Bingo board"""
    def __init__(self, data: array2D):
        self.grid = data
        self.n    = len(data[0])

    def __eq__(self, other):
        return self.grid == other.grid

    def __hash__(self):
        return hash((self.grid))

    def check_row(self, d: Mapping[int, bool]) -> bool:
        for row in self.grid:
            cnt = 0
            for x in row:
                if x in d:
                    cnt += 1
            if cnt == self.n:
                return True
        return False

    def check_col(self, d: Mapping[int, bool]) -> bool:
        for i in range(self.n):
            cnt = 0
            for j in range(self.n):
                x = self.grid[j][i]
                if x in d:
                    cnt += 1
            if cnt == self.n:
                return True
        return False

def score(b: Board, d: Mapping[int, bool], n: int) -> int:
    """Score is the product of the sum of marked board numbers and n"""
    cnt = 0
    for row in b.grid:
        for x in row:
            if x not in d:
                cnt += x
    return cnt * n

with open('input/day4.txt') as f:
    nums = [int(x) for x in f.readline().strip().split(',')]
    f.readline() # throwaway
    boards = []
    data   = []
    for line in f:
        if line != '\n':
            data.append([int(x) for x in line.split()])
        else:
            boards.append(Board(tuple(tuple(x) for x in data))) # use tuples because hashable
            data = []
    boards.append(Board(tuple((tuple(x) for x in data))))

    # Part One: first board to win
    d  = {}
    ok = False
    for x in nums:
        d[x] = True
        for b in boards:
            if (b.check_row(d) == True or b.check_col(d) == True) and ok == False:
                print(score(b, d, x))
                ok = True

    # Part Two: last board to win
    d     = {}
    ok    = False
    bingo = set()
    for x in nums:
        d[x] = True
        for b in boards:
            if b.check_row(d) == True or b.check_col(d) == True:
                bingo.add(b)
                if len(bingo) == len(boards) and ok == False:
                    print(score(b, d, x))
                    ok = True
