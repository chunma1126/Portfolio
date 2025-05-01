#include "SectorPattern.h"
#include "cocos2d.h"

SectorPattern::~SectorPattern()
{
}

void SectorPattern::start()
{
	_completeTime = 2.5f;
    _arrowCount = 6;
    _arrowSpeed = 15000;

    float centerAngleDeg = 90.0f + (90 * _direction);
    float sectorAngleDeg = 40.f;

    float startAngleDeg = centerAngleDeg - sectorAngleDeg / 2.0f;
    float endAngleDeg = centerAngleDeg + sectorAngleDeg / 2.0f;

    for (int i = 0; i < _arrowCount; ++i)
    {
        float t = (float)i / (_arrowCount - 1);
        float angleDeg = startAngleDeg + (endAngleDeg - startAngleDeg) * t;
        float angleRad = CC_DEGREES_TO_RADIANS(angleDeg);

        cocos2d::Vec2 dir(cosf(angleRad), sinf(angleRad));

        auto visibleSize = Director::getInstance()->getVisibleSize();
        Vec2 origin = Director::getInstance()->getVisibleOrigin();
        Vec2 screenCenter = { visibleSize.width / 2 + origin.x  ,visibleSize.height / 2 + origin.y };

        Vec2 pos;
        switch (_direction)
        {
        case 0:
            pos = Vec2(screenCenter.x, origin.y - _margin);
            break;
        case 1:
            pos = Vec2(visibleSize.width + _margin, screenCenter.y);
            break;
        case 2:
            pos = Vec2(screenCenter.x, visibleSize.height + _margin);
            break;
        case 3:
            pos = Vec2(origin.x - _margin, screenCenter.y);
            break;
        }

        ArrowPool::getInstance().pop(pos, dir, _arrowSpeed);
    }

}

void SectorPattern::update(float dt)
{
	_timer += dt;
}

void SectorPattern::reset()
{
}

bool SectorPattern::isCompleted()
{
	return _timer >= _completeTime;
}
