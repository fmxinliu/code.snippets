i = 0
while i < 10:
    i += 1

    if i == 5:
        continue

    if i == 7:
        print(f'i={i}，跳出')
        break

    print(i)

