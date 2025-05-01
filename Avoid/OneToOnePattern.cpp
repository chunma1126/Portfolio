#include "OneToOnePattern.h"

OneToOnePattern::~OneToOnePattern()
{

}

void OneToOnePattern::start()
{
    _arrowCount = 10;
    _arrowSpeed = 15000;
    _completeTime = 9;

    _margin = _size / 8;

    auto visibleSize = Director::getInstance()->getVisibleSize();
    _origin = Director::getInstance()->getVisibleOrigin();
    Vec2 screenCenter = Vec2(visibleSize.width / 2 + _origin.x, visibleSize.height / 2 + _origin.y);

    _startPosX = screenCenter.x - _size;
    _endPosX = screenCenter.x + _size;
    float emptySize = visibleSize.width + _origin.x - (_startPosX - _endPosX);

    float _halfSize = emptySize * 0.5f;
    _arrowInterval = _halfSize / _arrowCount;

    float emptyInterval = (_endPosX - _startPosX) / _oneShotCount;
    float halfInterval = emptyInterval * 0.5f;

    for (int i = 0; i < _oneShotCount; ++i)
    {
        _oneShoePos[i].x = _startPosX + halfInterval + emptyInterval * i;
        _oneShoePos[i].y = _origin.y;
    }
    
}

void OneToOnePattern::update(float dt)
{
    _timer += dt;
    _completeTimer += dt;
    _oneShotTimer += dt;

    if (_timer >= _fireTime) 
    {
        _timer = 0;
        for (int i = 0; i < _arrowInterval; i++)
        {
            float posX = _origin.x + (i * _arrowInterval) + _margin;

            if (posX > _startPosX && posX < _endPosX)
            {
                continue;  
            }

            ArrowPool::getInstance().pop({ posX, _origin.y }, Vec2::UNIT_Y, _arrowSpeed);
        }
    }

    if (_oneShotTimer >= _oneShotTime)
    {
        _oneShotTimer = 0;
        Vec2 pos = _oneShoePos[rand() % _oneShotCount];
        ArrowPool::getInstance().pop(pos, Vec2::UNIT_Y, _oneShotSpeed);
    }

}

void OneToOnePattern::reset()
{
}

bool OneToOnePattern::isCompleted()
{
    return _completeTimer >= _completeTime;
}
