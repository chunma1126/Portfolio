#pragma once
#include "cocos2d.h"
#include "HealthComponent.h"
#include <unordered_map>

class Player : public cocos2d::Node 
{
public:
    virtual bool init() override;
    virtual void update(float dt) override;
    virtual void onEnter() override;
    virtual void onExit() override;

    ~Player();
    CREATE_FUNC(Player);

public:
     HealthComponent& getHealthComponent() { return health; }
private:
    bool onCollisionBegin(cocos2d::PhysicsContact& contact);
    void move(float dt);
    void clampPosition();

    void FlashFeedback();
private:
    std::unordered_map<int, bool> _keyState;

    cocos2d::Sprite* _sprite;
    cocos2d::PhysicsBody* _rigidBody;

    cocos2d::Size visibleSize;
    cocos2d::Vec2 originSize;
    cocos2d::Vec2 screenCenter;

    HealthComponent health;
    
    cocos2d::EventListenerKeyboard* _keyboardListener = nullptr;
    cocos2d::EventListenerPhysicsContact* _contactListener = nullptr;

private:
    const float _collisionScale = 2.5f;
    const float _invincibilityTime = 1.5f;
    const int _flashCount = 6;
    float _flashTime = (_invincibilityTime / _flashCount) * 0.5f;

	float _speed = 10000;

    bool _canInput = true;
	bool _isMoving;
};

