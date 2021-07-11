#include "module2.h"
#include "gtest/gtest.h"

//#define __RUN_TEST_FAIL__

// 4.分支测试
TEST(FatalAssertionTest, Demo1)
{
#ifdef __RUN_TEST_FAIL__
    if (ERROR_ == RunFunc(-1)) {
        FAIL() << "Fatal:";
    }

    // 1.失败后，不会继续往下执行
    printf("222222222\n");

    if (FAILED_ == RunFunc(0)) {
        ADD_FAILURE() << "failed:";
    }

    // 2.失败后，可以继续往下执行
    printf("111111111\n");
#endif

    // 3.成功
    SUCCEED();
}


// 5.输出测试参数
TEST(PredicateAssertionTest, AcceptsTemplateFunction)
{
#ifdef __RUN_TEST_FAIL__
    int a = 5;
    int b = 6;

    // EXPECT_PRED1 ~ EXPECT_PRED5 共5个
    // 注意: 第一个参数如果是模板函数，必须添加括号
    EXPECT_PRED2((GreaterThan<int, int>), a, b);
#endif
}


// 6.自定义输出信息
template <typename T1, typename T2>
testing::AssertionResult AssertGreaterThan(const char* m_expr, const char* n_expr, T1 a, T2 b) {
    if (GreaterThan(a, b))
        return testing::AssertionSuccess();
    testing::Message msg;
    msg << "\n"
        //<< "预期： " << m_expr << " > " << n_expr << "\n"
        << "实际： " << m_expr << " <= " << n_expr;
    return testing::AssertionFailure(msg);
}

TEST(PredicateAssertionTest, AssertGreaterThan)
{
#ifdef __RUN_TEST_FAIL__
    // EXPECT_PRED_FORMAT1 ~ EXPECT_PRED_FORMAT5 共5个
    EXPECT_PRED_FORMAT2((AssertGreaterThan<int, int>), 2, 3);
#endif
}


// 7.子过程中使用断言
void Sub(void* p) {
    EXPECT_NE(0, (int)p); // 检查指针是否悬空
}

void Test1() {
#ifdef __RUN_TEST_FAIL__
    SCOPED_TRACE("==== Test1 =====");  // 调用 sub： 失败时输出
    Sub(NULL);
#endif
}

void Test2() {
    SCOPED_TRACE("==== Test2 =====");  // 调用 sub ： 失败时输出
    int a = 0;
    Sub(&a);
}

TEST(SubTest, Test1)
{
    // 调用了同一个子过程
    Test1();
    Test2();
}
