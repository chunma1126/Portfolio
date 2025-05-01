#include "Player.h"
#include "Enum.h"
#include "CameraShakeAction.h"

#define FLASH_COLOR 200,0,0
#define DEFAULT_COLOR 255,255,255

USING_NS_CC;

bool Player::init()
{
    if (!Node::init()) {
        return false;
    }
    
    visibleSize = Director::getInstance()->getVisibleSize();
    originSize = Director::getInstance()->getVisibleOrigin();
    
    Vec2 screenCenter = Vec2(visibleSize.width / 2 + originSize.x, visibleSize.height / 2 + originSize.y);

    setPosition(screenCenter);

    //sprite init
    {
        _sprite = Sprite::create("character.png");
        _sprite->setScale(0.2f);
        addChild(_sprite);
    }

    //rigidbody init
    {
        _rigidBody = PhysicsBody::createCircle(_collisionScale, PHYSICSBODY_MATERIAL_DEFAULT);
        _rigidBody->setGravityEnable(false);
        _rigidBody->setDynamic(true);

        _rigidBody->setCategoryBitmask(LayerMask::PLAYER);      
        //_rigidBody->setCollisionBitmask(LayerMask::ARROW);      
        _rigidBody->setContactTestBitmask(LayerMask::ARROW);

        _rigidBody->setTag(LayerMask::PLAYER);

        setPhysicsBody(_rigidBody);
    }

    //input init
    {
        _keyboardListener = EventListenerKeyboard::create();

        _keyboardListener->onKeyPressed = [=](EventKeyboard::KeyCode key, Event*)
            {
                if (_canInput)
                    _keyState[static_cast<int>(key)] = true;
            };

        _keyboardListener->onKeyReleased = [=](EventKeyboard::KeyCode key, Event*)
            {
                if (_canInput)
                    _keyState[static_cast<int>(key)] = false;
            };

    }
    
    //rigidbodyEvent init
    {
        _contactListener = EventListenerPhysicsContact::create();
        _contactListener->onContactBegin = std::bind(&Player::onCollisionBegin, this, std::placeholders::_1);
    }
    
    //flash feedbacks
    health.onDamageEvents.add([=]()
    {
        FlashFeedback();
    });

    //cameraShake
    health.onDamageEvents.add([=]()
    {
        auto shake = CameraShakeAction::create(0.21f, 9,9);
        Director::getInstance()->getRunningScene()->getDefaultCamera()->runAction(shake);
    });

    health.onDeadEvents.add([this]()
        {
            _canInput = false;
            
            for (auto& keyEvent : _keyState)
            {
                keyEvent.second = false;
            }

            _rigidBody->setVelocity(Vec2::ZERO);

        });

    scheduleUpdate();

    return true;
}

void Player::update(float dt)
{
    move(dt);
}

void Player::onEnter()
{
    Node::onEnter();

    _eventDispatcher->addEventListenerWithFixedPriority(_keyboardListener, 1);
    _eventDispatcher->addEventListenerWithFixedPriority(_contactListener, 1);
}

void Player::onExit()
{
    Node::onExit();

    _eventDispatcher->removeEventListener(_keyboardListener);
    _eventDispatcher->removeEventListener(_contactListener);
}

Player::~Player()
{

}

void Player::clampPosition()
{
    auto pos = getPosition();
    Size spriteSize = _sprite->getContentSize() * _sprite->getScale();

    float halfWidth = spriteSize.width / 2.0f;
    float halfHeight = spriteSize.height / 2.0f;

    float clampedX = clampf(pos.x, halfWidth + originSize.x, visibleSize.width - halfWidth + originSize.x);
    float clampedY = clampf(pos.y, halfHeight + originSize.y, visibleSize.height - halfHeight + originSize.y);

    setPosition(Vec2(clampedX, clampedY));
}

void Player::move(float dt)
{
    Vec2 velocity;

    if (_keyState[static_cast<int>(EventKeyboard::KeyCode::KEY_A)]) {
        velocity.x -= _speed;
    }

    if (_keyState[static_cast<int>(EventKeyboard::KeyCode::KEY_D)]) {
        velocity.x += _speed;
    }
    if (_keyState[static_cast<int>(EventKeyboard::KeyCode::KEY_W)]) {
        velocity.y += _speed;
    }
    if (_keyState[static_cast<int>(EventKeyboard::KeyCode::KEY_S)]) {
        velocity.y -= _speed;
    }

    if (velocity.length() > _speed)
    {
        velocity.normalize();
        velocity *= _speed;
    }
	
    if (velocity.length() > 0)
        _isMoving = true;
    else {
        _isMoving = false;
    }
    
    _rigidBody->setVelocity(velocity * dt);

    clampPosition();
}

bool Player::onCollisionBegin(cocos2d::PhysicsContact& contact)
{
    auto bodyA = contact.getShapeA()->getBody();
    auto bodyB = contact.getShapeB()->getBody();

    bool isCollision =
        (bodyA->getTag() == LayerMask::PLAYER && bodyB->getTag() == LayerMask::ARROW)
      ||(bodyA->getTag() == LayerMask::ARROW  && bodyB->getTag() == LayerMask::PLAYER);

    //check physicsbody
    if (isCollision) {
        health.takeDamage(2);
    }
    else {
        return false;
    }


    return false;
}

void Player::FlashFeedback()
{
    health.setInvincibility(true);
    auto disableInvinciblity = CallFunc::create([&](){ health.setInvincibility(false); });

    TintTo* defaultToRed  = TintTo::create(_flashTime, FLASH_COLOR);
    TintTo* redToDefault  = TintTo::create(_flashTime, DEFAULT_COLOR);

    auto flashFeedback = Sequence::create(defaultToRed , redToDefault,nullptr);
    auto repeatFlash = Repeat::create(flashFeedback,_flashCount);

    auto flash = Sequence::create(repeatFlash , disableInvinciblity , nullptr);

    _sprite->runAction(flash);
}