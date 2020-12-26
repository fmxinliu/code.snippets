#include <stdio.h>
#include <assert.h>
#include <memory.h>
#include <malloc.h>

#define LEN 10
#define OVERLAPLEN 3

void* mymemcpy(void *dst, const void *src, size_t size);
void* mymemmove(void *dst, const void *src, size_t size);

int main() {
    // 1: ���ص�
    char p[LEN + 1] = "JellyThink";
    char q[LEN + 1] = "";
    mymemcpy(q, p, LEN);
    puts(q);

    memset(q, 0, LEN);
    mymemmove(q, p, LEN);
    puts(q);

    // 2: ���ص�
    size_t len = 2 * LEN - OVERLAPLEN + 1;
    size_t offest = LEN - OVERLAPLEN;
    char *org = (char *)malloc(len * sizeof(char));
    memset(org, 0, len);
    memcpy(org, "JellyThink", LEN + 1);

    // srcβ����dstͷ�ص�
    char *src = org;
    char *dst = org + offest;
    mymemcpy(dst, src, LEN);
    puts(dst);

    memset(org, 0, len);
    memcpy(org, "JellyThink", LEN + 1);
    mymemmove(dst, src, LEN);
    puts(dst);

    // srcͷ����dstβ���ص�
    dst = org;
    src = org + offest;

    memset(org, 0, len);
    memcpy(src, "JellyThink", LEN + 1);
    mymemcpy(dst, src, LEN);
    puts(dst);

    memset(org, 0, len);
    memcpy(src, "JellyThink", LEN + 1);
    mymemmove(dst, src, LEN);
    puts(dst);

    getchar();
    free(org);
    return 0;
}


void* mymemcpy(void *dst, const void *src, size_t size) {
    assert(dst != NULL && src != NULL);

    if (src != dst) {
        char *p = (char *)dst;
        char *q = (char *)src;

        while (size--) {
            *p++ = *q++;
        }
    }

    return dst;
}

void* mymemmove(void *dst, const void *src, size_t size) {
    assert(dst != NULL && src != NULL);

    // ��ͷ����ʼ����
    if (src >= dst) {
        return mymemcpy(dst, src, size);
    }

    // ��β����ʼ����
    char *p = (char *)dst + (size - 1);
    char *q = (char *)src + (size - 1);

    while (size--) {
        *p-- = *q--;
    }

    return dst;
}
