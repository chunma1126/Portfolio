#include "Arrow.h"
#include "cocos2d.h"
#include "ArrowPool.h"
#include "Enum.h"

USING_NS_CC;

bool Arrow::init()
{
    if (!Node::init()) 
    {
        return false;
    }

    //sprite init
    {
        _sprite = Sprite::create("Arrow.png");
        _sprite->setScale(0.2f);
        _sprite->setOpacity(0);
        addChild(_sprite);
    }

    {
        int widht = _sprite->getContentSize().width * _sprite->getScale();
        int height = _sprite->getContentSize().height * _sprite->getScale();

        _rigidBody = PhysicsBody::createBox(Size(widht, height), PHYSICSBODY_MATERIAL_DEFAULT);

        _rigidBody->setGravityEnable(false);
        _rigidBody->setDynamic(true);

        _rigidBody->setCategoryBitmask(LayerMask::ARROW);        
        _rigidBody->setCollisionBitmask(LayerMask::PLAYER);    
        _rigidBody->setContactTestBitmask(LayerMask::PLAYER);    

        _rigidBody->setTag(LayerMask::ARROW);

        setPhysicsBody(_rigidBody);
    }

    {
        Size screenSize = Director::getInstance()->getVisibleSize();
        Vec2 originSize = Director::getInstance()->getVisibleOrigin();

        _maxBoundingSize = { screenSize.width + originSize.x + _margin  , screenSize.height + originSize.y + _margin };
        _minBoundingSize = { originSize.x - _margin ,originSize.y - _margin };
    }

    scheduleUpdate();
    return true;
}

void Arrow::update(float dt)
{
    rotate();
    move(dt);
}

void Arrow::fadeIn()
{
    auto fadeInAction = FadeIn::create(0.2f);
    _sprite->runAction(fadeInAction);
}

void Arrow::fadeOut()
{
    _sprite->setOpacity(0);
}

void Arrow::move(float dt)
{
    auto pos = getPosition();

    if (pos.x < _minBoundingSize.x || pos.x > _maxBoundingSize.x ||
        pos.y < _minBoundingSize.y || pos.y > _maxBoundingSize.y)
    {
        ArrowPool::getInstance().push(this);
        return;
    }

    _rigidBody->setVelocity(_direction * _speed * dt);
}

void Arrow::rotate()
{
    if (_ignoreRotate)return;

    Vec2 velocity = _rigidBody->getVelocity();
    velocity.normalize();
    float angle = CC_RADIANS_TO_DEGREES(velocity.getAngle());
    setRotation(-angle);
}




