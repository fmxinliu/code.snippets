a = 0
b = 1
c = 2

# 等价
print(a < b < c)
print(a < b and b < c)
print((a < b) and (b < c))

print(a < b or b > c)
print(not a < b)  # not 取非
print(not (a < b))

