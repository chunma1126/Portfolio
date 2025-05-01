#pragma once
#include "cocos2d.h"
#include <queue>
class ArrowPattern;
class GameScene : public cocos2d::Scene
{
public:
    static cocos2d::Scene* createScene();

    virtual bool init();
    virtual void update(float dt);
    CREATE_FUNC(GameScene);

    void setPhysicsWorld(cocos2d::PhysicsWorld* world) { _physicsWorld = world; }
private:
    cocos2d::PhysicsWorld* _physicsWorld;
    std::queue<ArrowPattern*> _patternQueue;
    ArrowPattern* currentPattern;
};

