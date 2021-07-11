#include "module3.h"

struct StudentPrivate {
    int id;
    std::string name;
    int age;
};

DbMgr::DbMgr() : id(-1) {
    mp = new std::map<int, Student>;
}

DbMgr::~DbMgr() {
    delete mp;
    mp = NULL;
}

void DbMgr::insert(Student stu) {
    if (mp->find(stu.d->id) == mp->end()) {
        id = stu.d->id;
        mp->insert(std::make_pair<int, Student>(id, stu));
    }
    else {
        mp->at(stu.d->id) = stu;
    }
}

Student DbMgr::getStudent(int id) {
    if (mp->find(id) != mp->end()) {
        return mp->at(id);
    }

    return Student();
}

Student DbMgr::getStudent(std::string name) {
    std::map<int, Student>::const_iterator it = mp->cbegin();
    while (it != mp->cend()) {
        if (it->second.d->name == name) {
            return it->second;
        }
        it++;
    }

    return Student();
}

Student::Student() : d(new StudentPrivate) {
    d->id = -1;
    d->name = "";
    d->age = -1;
}

Student::Student(int id, std::string name, int age) : d(new StudentPrivate) {
    d->id = id;
    d->name = name;
    d->age = age;
}

Student::Student(const Student& stu) : d(new StudentPrivate) {
    d->id = stu.d->id;
    d->name = stu.d->name;
    d->age = stu.d->age;
}

Student::~Student() {
    delete d;
    d = NULL;
}

bool Student::operator==(const Student& stu) {
    return
        (this == &stu) || (
        (d->id == stu.d->id) &&
        (d->name == stu.d->name) &&
        (d->age == stu.d->age));
}

bool Student::operator!=(const Student& stu) {
    return !(*this == stu);
}

Student& Student::operator=(const Student& stu) {
    if (this != &stu) {
        d->id = stu.d->id;
        d->name = stu.d->name;
        d->age = stu.d->age;
    }

    return *this;
}

int Student::id() const { return d->id; }
