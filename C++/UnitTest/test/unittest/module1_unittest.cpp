//////////////////////////////////////////////////////////////////////////
// 第一步: 引入头文件、gtest库、测试模块
//////////////////////////////////////////////////////////////////////////
#include "module1.h"
#include "gtest/gtest.h"
#include <vector>
//#pragma comment(lib, "gtest_main.lib")


//////////////////////////////////////////////////////////////////////////
// 第二步: 定义测试用例
//////////////////////////////////////////////////////////////////////////
TEST(FooTest, HandleNoneZeroInput)
{
    EXPECT_EQ(2, Foo(4, 10));
    EXPECT_EQ(6, Foo(30, 18));
}


// 1.测试循环: 输入索引信息，否则失败后不好定位信息
TEST(RoundTest, HandleIntInput)
{
    std::vector<double> d;
    d.push_back(-2);
    d.push_back(-1);
    d.push_back(0);
    d.push_back(1);
    d.push_back(2);

    for (int i = 0; i < (int)d.size(); i++)
    {
        EXPECT_DOUBLE_EQ(Round_1(d[i], 0), Round_2(d[i], 0))
            << "Round_1() and Round_2() differ at index " << i;
    }
}

TEST(RoundTest, HandleDoubleInput)
{
    std::vector<double> d;
    d.push_back(-2.1);
    d.push_back(-1.5);
    d.push_back(0.51);
    d.push_back(1.3);
    d.push_back(2.7);

    for (int i = 0; i < (int)d.size(); i++)
    {
        EXPECT_DOUBLE_EQ(Round_1(d[i], 0), Round_2(d[i], 0))
            << "Round_1() and Round_2() differ at index " << i;
    }
}

TEST(RoundTest, HandleDoubleInput2)
{
    std::vector<double> d;
    d.push_back(-2.17);
    d.push_back(-1.54);
    d.push_back(0.519);
    d.push_back(1.92);
    d.push_back(2.96);

    for (int i = 0; i < (int)d.size(); i++)
    {
        EXPECT_DOUBLE_EQ(Round_1(d[i], 1), Round_2(d[i], 1))
            << "Round_1() and Round_2() differ at index " << i;
    }
}

// 2.测试异常
TEST(ThrowExceptionTest, Check)
{
    // 抛出指定类型异常
    EXPECT_THROW(ThrowException(0), int);
    EXPECT_THROW(ThrowException(1), char*);
    EXPECT_THROW(ThrowException(2), float);

    EXPECT_ANY_THROW(ThrowException(2));  // 抛出任意类型异常
    EXPECT_NO_THROW(ThrowException(3));   // 不抛出异常
}

// 3.测试字符串
TEST(StringCmpTest, Demo)
{
    char* pszCoderZh = "CoderZh";
    wchar_t* wszCoderZh = L"CoderZh";
    std::string strCoderZh = "CoderZh";
    std::wstring wstrCoderZh = L"CoderZh";

    EXPECT_STREQ("CoderZh", pszCoderZh);
    EXPECT_STREQ(L"CoderZh", wszCoderZh);

    EXPECT_STRNE("CnBlogs", pszCoderZh);
    EXPECT_STRNE(L"CnBlogs", wszCoderZh);

    EXPECT_STREQ("CoderZh", strCoderZh.c_str());
    EXPECT_STREQ(L"CoderZh", wstrCoderZh.c_str());

    // 不区分大小写
    EXPECT_STRCASEEQ("coderzh", pszCoderZh);
    //EXPECT_STRCASEEQ(L"coderzh", wszCoderZh);   // 不支持 wchar_t*
}

//////////////////////////////////////////////////////////////////////////
// 第三步: main()中启动测试，可以链接gtest_main.lib中导出的main()
//////////////////////////////////////////////////////////////////////////
//int main(int argc, char **argv)
//{
//    testing::InitGoogleTest(&argc, argv);
//    return RUN_ALL_TESTS();
//}