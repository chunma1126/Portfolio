#pragma once
#include "cocos2d.h"

USING_NS_CC;

class Arrow : public cocos2d::Node 
{
public:
    virtual bool init() override;
    virtual void update(float dt) override;

    void fadeIn();
    void fadeOut();

    void move(float dt);
    void setIngnoreRotate(bool ignore) { _ignoreRotate = ignore; };
    void setDirectionAndSpeed(Vec2 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }

    CREATE_FUNC(Arrow);
private:
    void rotate();


private:
    PhysicsBody* _rigidBody;
    Sprite* _sprite;
    
    Vec2 _maxBoundingSize;
    Vec2 _minBoundingSize;

    Vec2 _direction;
    float _speed;

    float _margin = 150;

    bool _ignoreRotate = false;
};

