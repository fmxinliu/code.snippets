#include "module2.h"
#include "gtest/gtest.h"

//#define __RUN_TEST_FAIL__

// 4.��֧����
TEST(FatalAssertionTest, Demo1)
{
#ifdef __RUN_TEST_FAIL__
    if (ERROR_ == RunFunc(-1)) {
        FAIL() << "Fatal:";
    }

    // 1.ʧ�ܺ󣬲����������ִ��
    printf("222222222\n");

    if (FAILED_ == RunFunc(0)) {
        ADD_FAILURE() << "failed:";
    }

    // 2.ʧ�ܺ󣬿��Լ�������ִ��
    printf("111111111\n");
#endif

    // 3.�ɹ�
    SUCCEED();
}


// 5.������Բ���
TEST(PredicateAssertionTest, AcceptsTemplateFunction)
{
#ifdef __RUN_TEST_FAIL__
    int a = 5;
    int b = 6;

    // EXPECT_PRED1 ~ EXPECT_PRED5 ��5��
    // ע��: ��һ�����������ģ�庯���������������
    EXPECT_PRED2((GreaterThan<int, int>), a, b);
#endif
}


// 6.�Զ��������Ϣ
template <typename T1, typename T2>
testing::AssertionResult AssertGreaterThan(const char* m_expr, const char* n_expr, T1 a, T2 b) {
    if (GreaterThan(a, b))
        return testing::AssertionSuccess();
    testing::Message msg;
    msg << "\n"
        //<< "Ԥ�ڣ� " << m_expr << " > " << n_expr << "\n"
        << "ʵ�ʣ� " << m_expr << " <= " << n_expr;
    return testing::AssertionFailure(msg);
}

TEST(PredicateAssertionTest, AssertGreaterThan)
{
#ifdef __RUN_TEST_FAIL__
    // EXPECT_PRED_FORMAT1 ~ EXPECT_PRED_FORMAT5 ��5��
    EXPECT_PRED_FORMAT2((AssertGreaterThan<int, int>), 2, 3);
#endif
}


// 7.�ӹ�����ʹ�ö���
void Sub(void* p) {
    EXPECT_NE(0, (int)p); // ���ָ���Ƿ�����
}

void Test1() {
#ifdef __RUN_TEST_FAIL__
    SCOPED_TRACE("==== Test1 =====");  // ���� sub�� ʧ��ʱ���
    Sub(NULL);
#endif
}

void Test2() {
    SCOPED_TRACE("==== Test2 =====");  // ���� sub �� ʧ��ʱ���
    int a = 0;
    Sub(&a);
}

TEST(SubTest, Test1)
{
    // ������ͬһ���ӹ���
    Test1();
    Test2();
}
