#include "module4.h"
#include "gtest/gtest.h"

// ����������ࡣ
// 1.
class IsPrimeTest : public ::testing::TestWithParam<int> {};
class IsNonPrimeTest : public ::testing::TestWithParam<int> {};
// 2.
class IsEvenTest : public ::testing::TestWithParam<int> {};
// 3.
class IsSucTest : public ::testing::TestWithParam<bool> {};

 TEST_P(IsPrimeTest, Prime)
 {
     Utils obj;
     int n = GetParam();
     EXPECT_TRUE(obj.IsPrime(n));
 }

 TEST_P(IsNonPrimeTest, NonPrime)
 {
     Utils obj;
     int n = GetParam();
     EXPECT_FALSE(obj.IsPrime(n));
 }

 TEST_P(IsEvenTest, EvenTest)
 {
     Utils obj;
     int n = GetParam();
     if (n % 2 == 0) {
         EXPECT_TRUE(obj.IsEven(n));
     }
     else {
         EXPECT_FALSE(obj.IsEven(n));
     }  
 }

 // ��һ�������ǲ���ǰ׺����������ȡ�� 
 // �ڶ��������ǲ�����������Ҫ��֮ǰ����Ĳ��������������ͬ
 // �����������ǲ���������������һϵ�в��Բ���
INSTANTIATE_TEST_CASE_P(Prime, IsPrimeTest, testing::Values(3, 5, 11, 23, 17));
INSTANTIATE_TEST_CASE_P(NonPrime, IsNonPrimeTest, testing::Values(-9, -1, 0, 1, 4));


int even_values[] = {66, -88, 0};
int odd_values[] = {-53, 71, 83, 91, 101};
INSTANTIATE_TEST_CASE_P(Even_Values, IsEvenTest, testing::ValuesIn(even_values));
INSTANTIATE_TEST_CASE_P(Odd_Values, IsEvenTest, testing::ValuesIn(odd_values));
INSTANTIATE_TEST_CASE_P(EvenTest, IsEvenTest, testing::Range(-10, 10, 2));


INSTANTIATE_TEST_CASE_P(Suc, IsSucTest, testing::Bool());
