#include "module2.h"
#include <map>

static /* 必须添加: 文件作用域 */
    std::map<int, std::string> mp;


std::string RunFunc(int ret)
{
    if (mp.empty())
    {
        mp.insert(std::pair<int, std::string>(1, SUCCESS_));
        mp.insert(std::pair<int, std::string>(0, FAILED_));
        mp.insert(std::pair<int, std::string>(-1, ERROR_));
        //mp.insert(std::make_pair<int, std::string>(0, "failed"));
        //mp.insert(std::map<int, std::string>::value_type(-1, "fatal"));
        //mp[-1] = "fatal";
    }

    if (ret > 0)
        return mp[1];
    else if (ret < 0)
        return mp[-1];
    else
        return mp[0];
}
