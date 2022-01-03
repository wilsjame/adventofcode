from typing import Union
import utils
a = utils.get_input(day=3, data_type=str, sep='\n', preview_lines=6)

def numofbits(i: int, a: Union[tuple, list]) -> int:
    """returns the number of 1 bits in position i from every element in a""" 
    res = 0
    for x in a:
        res += x[i] == '1'
    return res

# Part One
n = len(a[0])
gamma   = [] 
epsilon = []
for i in range(n):
    one  = numofbits(i, a)
    zero = len(a) - one
    if one > zero:
        gamma   += '1'
        epsilon += '0'
    else:
        gamma   += '0'
        epsilon += '1'
gamma = int(''.join(gamma), 2)
epsilon = int(''.join(epsilon), 2)
print(gamma * epsilon)

# Part Two
# yeaahh utils.get_input should probably return a list instead of a tuple...
entries = []
for x in a:
    entries.append(x)

def sieve(a: list, i: int, b: int) -> int:
    """removes entries in a with bit b in position i"""
    res = []
    for x in a:
        if int(x[i]) == b:
            continue
        res.append(x)
    return res

def dfs_oxygen(a: list(), i: int) -> int:
    if len(a) == 1:
        return int(a[0], 2)
    else:
        one = numofbits(i, a)
        zero = len(a) - one
        if one >= zero:
            # keep entries with one bit in position i
            a = sieve(a, i, 0)
        else:
            # keep entries with zero bit in position i
            a = sieve(a, i, 1)
        return dfs_oxygen(a, i + 1)

def dfs_c02(a: list(), i: int) -> int:
    if len(a) == 1:
        return int(a[0], 2)
    else:
        one = numofbits(i, a)
        zero = len(a) - one
        if zero <= one:
            # keep entries with zero bit in position i
            a = sieve(a, i, 1)
        else:
            # keep entries with one bit in position i
            a = sieve(a, i, 0)
        return dfs_c02(a, i + 1)

O = dfs_oxygen(entries, 0)
C02 = dfs_c02(entries, 0)
print(O * C02)
