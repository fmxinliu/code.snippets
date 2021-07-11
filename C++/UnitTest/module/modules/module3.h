#ifndef MODULE3_H
#define MODULE3_H

#include "libcommon.h"
#include <map>
#include <string>

class DbMgr;
struct StudentPrivate;
class MODULE_API Student {
public:
    Student();
    Student(const Student& stu);
    Student(int id, std::string name, int age);
    Student& operator=(const Student& stu);
    ~Student();
    bool operator==(const Student& stu);
    bool operator!=(const Student& stu);
    int id() const;

private:
    StudentPrivate *d;
    friend DbMgr;
};


class MODULE_API DbMgr {
public:
    DbMgr();
    ~DbMgr();
    void insert(Student stu);
    Student getStudent(int id);
    Student getStudent(std::string name);

private:
    std::map<int, Student>* mp;
    int id;
};

#endif  // MODULE3_H
