Image = list[list[str]]
with open('input/day20.txt') as f:
    ll = [x for x in f.read().strip().split('\n\n')]
algorithm: list[str] = [x for x in ll[0]]
image: Image = [[x for x in l] for l in ll[1].split('\n')]

def add_border(im: Image, default='.') -> Image:
    """add 3X padding border around image"""
    new_len: int = len(im[0]) + 6
    new_im: Image = [[default] * new_len] * 3
    for row in im:
        new_im.append([default] * 3 + row + [default] * 3)
    new_im.extend([[default] * new_len] * 3)
    return new_im

def idx(im: Image, i: int, j: int, default: str = '.') -> int:
    """"returns pixel at i, j"""
    if i < 0 or j < 0 or i >= len(im) or j >= len(im[0]):
        return default
    return im[i][j]

def code(im: Image, i: int, j: int, default: str = '.') -> int:
    """returns pixel code for the center of a 3x3 kernel matrix"""
    seq: list[str] = []
    for ii in range(i-1, i+2):
        for jj in range(j-1, j+2):
            seq.append(idx(im, ii, jj, default))
    return int(''.join(seq).replace('.', '0').replace('#', '1'), 2)

# Part One and Two
p1: int = 2
p2: int = 50
for i in range(p2):
    d = ['.', '#'][i % 2]
    image = add_border(image, d)
    new_image: Image = []
    for ii in range(len(image)):
        r: list[str] = []
        for jj in range(len(image[ii])):
            r.append(algorithm[code(image, ii, jj, d)])
        new_image.append(r)
    image = new_image

cnt = 0
for i in range(len(image)):
    for j in range(len(image[i])):
        cnt += image[i][j] == '#'
print(cnt)
