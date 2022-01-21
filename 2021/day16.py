import math
from itertools import chain
s: str
with open('input/day16.txt') as f:
    s = f.read().strip()

nibs: list[str] = []
nibs = [bin(int(nib, 16))[2:].zfill(4) for nib in s]
b = list(chain.from_iterable(nibs)) # bit dump

def header(bits: list[str]) -> tuple[int, int]:
    """return packet version and type ID [1-7] from 6-bit header"""
    v = int(''.join(bits[:3]), 2)
    t = int(''.join(bits[3:6]), 2)
    return v, t

def literal(bits: list[str], ptr: int) -> int:
    """literal value contained in a packet"""
    b: list[str] = []
    while bits[ptr] == '1':
        b.append(bits[ptr+1:ptr+5])
        ptr += 5
    b.append(bits[ptr+1:ptr+5])
    ptr += 5
    l = int(''.join(list(chain.from_iterable(b))), 2)
    a.append(l)
    return ptr

def operator0(bits: list[str], ptr: int, op: str) -> int:
    """next 15 bits are a number representing total bit length of sub-packets contained in this packet"""
    l = int(''.join(bits[ptr:ptr+15]), 2)
    ptr += 15
    bcnt = 0
    a.append('(')
    a.append(op)
    while bcnt < l:
        k, b = parse(bits, ptr)
        bcnt += b
        ptr += b
    a.append(')')
    return ptr

def operator1(bits: list[str], ptr: int, op: str) -> int:
    """next 11 bits are a number representing number of sub-packets contained by this packet"""
    l = int(''.join(bits[ptr:ptr+11]), 2)
    ptr += 11
    pcnt = 0
    a.append('(')
    a.append(op)
    while pcnt < l:
        k, b = parse(bits, ptr)
        pcnt += k
        ptr += b
    a.append(')')
    return ptr

def parse(bits: list[str], ptr) -> tuple[int, int]:
    """returns tuple containing 1 as in one packet procssed and number of bits processed"""
    op = ['+', '*', 'mn', 'mx', '_', '>', '<', '=']
    ptr_i = ptr
    v, t = header(bits[ptr:ptr+6]) # version and type header
    global cnt
    cnt += v
    ptr += 6
    if t == 4:
        ptr = literal(bits, ptr)
    else:
        i = bits[ptr]
        ptr += 1
        if i == '0':
            ptr = operator0(bits, ptr, op[t])
        elif i == '1':
            ptr = operator1(bits, ptr, op[t])
    return 1, ptr - ptr_i

# Part One 
cnt:  int    = 0  # version sum
a: list[int] = [] # literal values and ops expression 
parse(b, 0)
print(cnt)

#TODO Part Two
# parse expression 
print(a)
