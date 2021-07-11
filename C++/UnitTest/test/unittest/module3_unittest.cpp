#include "module3.h"
#include "gtest/gtest.h"

//////////////////////////////////////////////////////////////////////////
// 全局事件: 在所有测试执行之前/后执行，需要在 main() 中挂载:
// testing::AddGlobalTestEnvironment(new FooEnvironment);
//////////////////////////////////////////////////////////////////////////
class FooEnvironment : public testing::Environment {
public:
    virtual void SetUp() {
        std::cout << "Foo FooEnvironment SetUp" << std::endl;
    }
    virtual void TearDown() {
        std::cout << "Foo FooEnvironment TearDown" << std::endl;
        show_statistical_info();
    }

private:
    void show_statistical_info() {
        // 统计: 全部测试用例执行情况
        testing::UnitTest* unit_test = testing::UnitTest::GetInstance();

        for (int i = 0; i < unit_test->total_test_case_count(); ++i) {
            int failed_tests = 0;
            int suc_tests = 0;
            const testing::TestCase* test_case = unit_test->GetTestCase(i);

            for (int j = 0; j < test_case->total_test_count(); ++j) {
                const testing::TestInfo* test_info = test_case->GetTestInfo(j);
                if (test_info->result()->Failed()) {
                    failed_tests++;
                }
                else {
                    suc_tests++;
                }
            }
            printf("%s. Suc: %d, Failed: %d\n", test_case->name(), suc_tests, failed_tests);
        }
    }
};


//////////////////////////////////////////////////////////////////////////
// TestSuite事件: 在全部TEST_F(类名, XXX)，即一个测试用例集测试之前/后执行
//////////////////////////////////////////////////////////////////////////
class FooSuiteTest : public testing::Test {
protected:
    static void SetUpTestCase() {
        testing::UnitTest* unit_test = testing::UnitTest::GetInstance();
        const testing::TestCase* test_case = unit_test->current_test_case();
        printf("%s Start.\n", test_case->name());

        s_instance = new DbMgr;
        //shared_resource_ = new T;
    }

    static void TearDownTestCase() {
        delete s_instance;
        s_instance = NULL;
        //delete shared_resource_;
        //shared_resource_ = NULL;

        show_statistical_info();
    }

    // Some expensive resource shared by all tests.
    //static T* shared_resource_;
    static DbMgr* s_instance;

private:
    static void show_statistical_info() {
        // 统计: 测试用例集执行情况
        testing::UnitTest* unit_test = testing::UnitTest::GetInstance();
        const testing::TestCase* test_case = unit_test->current_test_case();
        int failed_tests = 0;
        int suc_tests = 0;

        for (int j = 0; j < test_case->total_test_count(); ++j) {
            const testing::TestInfo* test_info = test_case->GetTestInfo(j);
            if (test_info->result()->Failed()) {
                failed_tests++;
            }
            else {
                suc_tests++;
            }
        }
        printf("%s End. Suc: %d, Failed: %d\n", test_case->name(), suc_tests, failed_tests);
    }
};

DbMgr* FooSuiteTest::s_instance = NULL;

TEST_F(FooSuiteTest, Test1)
{
    // you can refer to shared_resource here
}

TEST_F(FooSuiteTest, StudentCmp)
{
    Student stu1(1010, "01", 22);
    Student stu2(1010, "01", 22);

    EXPECT_TRUE(stu1 == stu1);
    EXPECT_TRUE(stu1 == stu2);

    EXPECT_FALSE(stu1 != stu1);
    EXPECT_FALSE(stu1 != stu2);
}

TEST_F(FooSuiteTest, StudentCopy)
{
    Student stu3(1111, "99", 77);

    Student stu4 = stu3; // 调用 Student 拷贝构造函数
    ASSERT_TRUE(stu3 == stu4);

    Student stu5;
    stu5 = stu3; // 调用 Student 赋值操作符
    ASSERT_TRUE(stu3 == stu5);

    stu3 = stu3;
    ASSERT_TRUE(stu3 == stu4);
}

TEST_F(FooSuiteTest, Insert)
{
    Student stu1(111, "小王", 18);
    s_instance->insert(stu1);

    Student stu2 = s_instance->getStudent(111);
    EXPECT_TRUE(stu1 == stu2);

    Student stu3 = s_instance->getStudent("小王");
    EXPECT_TRUE(stu1 == stu3);
}

TEST_F(FooSuiteTest, RepeatInsert)
{
    Student stu1(111, "小灰", 22);
    s_instance->insert(stu1);

    Student stu2 = s_instance->getStudent(111);
    EXPECT_TRUE(stu1 == stu2);
}

TEST_F(FooSuiteTest, QueryNonExist)
{
    Student stu1(222, "小王", 22);
    s_instance->insert(stu1);

    Student stu2 = s_instance->getStudent(333);
    EXPECT_TRUE(stu2.id() == -1);
}


//////////////////////////////////////////////////////////////////////////
// TestCase事件: 在每个TEST_F(类名, XXX)，即一条测试用例测试之前/后执行
//////////////////////////////////////////////////////////////////////////
class FooCaseTest:public testing::Test {
protected:
    virtual void SetUp() {
        std::cout << "Foo FooCaseTest start" << std::endl;
        data = 0;
    }
    virtual void TearDown() {
        std::cout << "Foo FooCaseTest end" << std::endl;
    }

    // exclusive by one test.
    int data;
};


TEST_F(FooCaseTest, First)
{
    EXPECT_EQ(data, 0);
    data = 1;
    EXPECT_EQ(data, 1);
}

TEST_F(FooCaseTest, Second)
{
    EXPECT_EQ(data, 0);
    data = 1;
    EXPECT_EQ(data, 1);
}
