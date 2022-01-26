A: list[list[str]] = []
with open('input/day18.txt') as f:
    for line in f:
        line = line.strip()
        a: list[str] = []
        i: int = 0
        while i < len(line):
            if line[i].isdigit():
                n: list[str] = []
                while line[i].isdigit():
                    n.append(line[i])
                    i += 1
                a.append(''.join(n))
            else:
                a.append(line[i])
                i += 1
        A.append(a)

def combine(l: list[str], r: list[str]) -> list[str]:
    """combine two snail numbers"""
    return ['['] + l + [','] + r + [']']

def nestdepth(a: list[str], ptr: int) -> int:
    """returns the nested depth of a pair at ptr"""
    cnt = 0
    for i in range(ptr, -1, -1):
        cnt += a[i] == '['
        cnt -= a[i] == ']'
    return cnt - 1

def findpr(a: list[str], anydepth: bool = False) -> int:
    """returns '[' ptr of a pair at any depth or nested depth >= 4"""
    for i in range(len(a) - 3):
        if a[i].isdigit() and a[i+1] == ',' and a[i+2].isdigit():
            if anydepth == True:
                return i - 1
            elif nestdepth(a, i) >= 4:
                return i - 1
    return -1

# mmm pasta :) 
def explode(a: list[str], ptr: int) -> list[str]:
    """explodes pair at ptr"""
    # ptr +1 +2 +3 +4
    #  [  x  ,  y  ]
    l, r = int(a[ptr+1]), int(a[ptr+3])
    # update nearest left value
    for i in range(ptr-1, -1, -1):
        if a[i].isdigit():
            a[i] = str(int(a[i]) + l)
            break
    # update nearest right value
    for i in range(ptr+4, len(a)):
        if a[i].isdigit():
            a[i] = str(int(a[i]) + r)
            break
    # zero original pair
    # ptr +1 +2 +3 +4
    #  [  x  ,  y  ]
    a[ptr+1] = str(0)
    del a[ptr+2:ptr+4]
    # ptr +1 +2 
    #  [  0  ]
    # remove original right and left brackets
    del a[ptr+2]
    del a[ptr]
    return a

def findsplit(a: list[str]) -> int:
    """returns ptr to left most number to split """
    for i in range(len(a)):
        if a[i].isdigit() and int(a[i]) > 9:
            return i
    return -1

def split(a: list[str], ptr) -> list[str]:
    """split number at ptr"""
    m = int(a[ptr])//2
    n = (int(a[ptr])+2-1)//2
    del a[ptr]
    a.insert(ptr, '[')
    a.insert(ptr+1, str(m))
    a.insert(ptr+2, ',')
    a.insert(ptr+3, str(n))
    a.insert(ptr+4, ']')
    return a

def reduce(a: list[str]) -> list[str]:
    """reduces a until no more explodes or splits can be done"""
    ptr = findpr(a)
    if ptr > -1:
        return reduce(explode(a, ptr))
    ptr = findsplit(a)
    if ptr > -1:
        return reduce(split(a, ptr))
    return a

def mag(a: list[str]) -> list[str]:
    """return magnitude of a"""
    if len(a) > 1:
        ptr = findpr(a, anydepth=True)
        m = int(a[ptr+1])*3 + int(a[ptr+3])*2
        del a[ptr:ptr+5]
        a.insert(ptr, str(m))
        return mag(a)
    return a

# Part One
res = reduce(combine(A[0], A[1]))
for i in range(2, len(A)):
    res = reduce(combine(res, A[i]))
print(*mag(res), sep='')

# Part Two
mx = -1
for i in range(len(A)):
    for j in range(len(A)):
        if i == j:
            continue
        else:
            mx = max(mx, int(mag(reduce(combine(A[i], A[j])))[0]))
print(mx)
