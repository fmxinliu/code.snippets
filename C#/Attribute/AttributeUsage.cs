using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttributeTest {
    class AttributeUsage {
        public static void Test() {
            CanWriteCheck(new ChildAccount());
            CanWriteCheck(new AdultAccount());

            // 演示: 在没有应用AccountsAttribute的类型上，方法也能正确地工作
            CanWriteCheck(new AttributeUsage());
        }

        #region 3.AttributeUsage检测
        static void CanWriteCheck(Object obj) {
            // 构造Attribute类型的一个实例，并初始化成我们要显示查找的内容
            Attribute checking = new AccountsAttribute(Accounts.Checking);

            // 构造应用于类型的特性实例
            Attribute validAccounts = Attribute.GetCustomAttribute(
                obj.GetType(), typeof(AccountsAttribute), false);
            //Object[] attrs = obj.GetType().GetCustomAttributes(false);
            //Attribute validAccounts = null;
            //if (attrs.Length > 0) {
            //    validAccounts = attrs[0] as Attribute;
            //}

            // 如果向精英应用了特性，而且指定了Checking账户，表明该类型可以开支票
            if ((validAccounts != null) && checking.Match(validAccounts)) {
                Console.WriteLine("{0} types can write checks.", obj.GetType());
            }
            else {
                Console.WriteLine("{0} types can NOT write checks.", obj.GetType());
            }
        }
        #endregion

        #region 2.AttributeUsage关联目标
        [AccountsAttribute(Accounts.Savings)]
        sealed class ChildAccount { }

        [AccountsAttribute(Accounts.Savings | Accounts.Checking | Accounts.Brokerage)]
        sealed class AdultAccount { }
        #endregion

        #region 1.AttributeUsage特性定义
        [Flags]
        enum Accounts {
            Savings = 0x0001,
            Checking = 0x0002,
            Brokerage = 0x0004,
        }

        [AttributeUsage(AttributeTargets.Class)]
        sealed class AccountsAttribute : Attribute {
            private Accounts accounts;

            public AccountsAttribute(Accounts accounts) {
                this.accounts = accounts;
            }

            public override bool Match(object obj) {
                // 如果基类实现了Match，而且基类不为Attribute，可以取消注释
                //if (!base.Match(obj)) return false;

                // this一定不为null，obj为null，一定不匹配
                if (obj == null) return false;

                // 不同类型，不匹配
                if (this.GetType() != obj.GetType()) return false;

                // 由于类型相同，转型一定成功
                AccountsAttribute other = (AccountsAttribute)obj;

                // 判断: this账户是不是other账号的一个子集
                if ((other.accounts & this.accounts) != accounts) {
                    return false;
                }

                return true; // 对象匹配
            }

            public override bool Equals(object obj) {
                // 如果基类实现了Equals，而且基类不为Object，可以取消注释
                //if (!base.Equals(obj)) return false;

                // this一定不为null，obj为null，一定不匹配
                if (obj == null) return false;

                // 不同类型，不匹配
                if (this.GetType() != obj.GetType()) return false;

                // 由于类型相同，转型一定成功
                AccountsAttribute other = (AccountsAttribute)obj;

                // 比较字段（Attribute是通过反射比较，效率低）
                // 判断: this账户是不是other账号
                if (other.accounts != this.accounts) {
                    return false;
                }

                return true; // 对象相等
            }

            // 重写GetHashCode，因为我们重写了Equals
            public override Int32 GetHashCode() {
                return (Int32)accounts;
            }
        }
        #endregion
    }
}
