def swap(a, b):
    return b, a


x, y = 1, 2
y, x = x, y
print(f'{x}, {y}')

aa, bb = 1, 2
aa, bb = swap(aa, bb)
print(f'{aa}, {bb}')
