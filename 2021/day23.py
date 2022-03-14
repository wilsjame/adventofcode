""" 
sample input
#############
#...........# <- hallway
###B#C#B#D### <- sideroom top
  #A#D#C#A# <--- sideroom bottom
  #########

we will index the layout as 1D
[11 + 4 * 2]
ex) the left and right hallway sides are 0 and 10
    the rightmost sideroom top and bottom are 14 and 18 

this type of graph search problem is like Magic Squares
from USACO training 3.2 which was from IOI'96

we brute force board positions and can use breadth first
search to find the cheapest winning path. in other words,
this is a shortest path problem, where the nodes of the 
graph are board arrangements, and edges are transformations.
breadth first search works because all edges are of unit length
(cost = 1)

notes: keep track of min cost (energy) for board positions
       implementing the movement is the tough part
"""

# hard code input here
sample: dict[int, str] = {
        0: '.', 1: '.', 2: '.', 3: '.', 4: '.', 5: '.', 6: '.', 7: '.', 8: '.', 9: '.', 10: '.',
        11: 'B', 12: 'C', 13: 'B', 14: 'D',
        15: 'A', 16: 'D', 17: 'C', 18: 'A'
        }

win: dict[int, str] = {
        0: '.', 1: '.', 2: '.', 3: '.', 4: '.', 5: '.', 6: '.', 7: '.', 8: '.', 9: '.', 10: '.',
        11: 'A', 12: 'B', 13: 'C', 14: 'D',
        15: 'A', 16: 'B', 17: 'C', 18: 'D'
        }

class Board:
    def __init__(self, pos):
        self.pos = pos
        self.cost = 1e9

    def __hash__(self):
        return hash(self.pos)

    def __eq__(self, other):
        return self.pos == other.pos

    def print(self):
        #print(f'cost: {self.cost}')
        print('#############')
        print(f'#{self.pos[0]}{self.pos[1]}{self.pos[2]}{self.pos[3]}{self.pos[4]}{self.pos[5]}{self.pos[6]}{self.pos[7]}{self.pos[8]}{self.pos[9]}{self.pos[10]}#')
        print(f'###{self.pos[11]}#{self.pos[12]}#{self.pos[13]}#{self.pos[14]}###')
        print(f'  #{self.pos[15]}#{self.pos[16]}#{self.pos[17]}#{self.pos[18]}#')
        print('  #########\n')

# TODO movements


# scratch
b1 = Board(sample)
b1.print()
b2 = Board(sample)
b2.print()
b3 = Board(win)
b3.print()
print(b2 == b2) # True
print(b1 == b3) # False
