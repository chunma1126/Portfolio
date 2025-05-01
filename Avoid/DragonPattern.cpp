#include "DragonPattern.h"

DragonPattern::~DragonPattern()
{

}

void DragonPattern::start()
{
	_completeTime = 10;
	
	visibleSize = Director::getInstance()->getVisibleSize();
	Vec2 origin = Director::getInstance()->getVisibleOrigin();
	screenCenter = Vec2(visibleSize.width / 2 + origin.x, visibleSize.height / 2 + origin.y);

}

void DragonPattern::update(float dt)
{
	_timer += dt;
	_fireTimer += dt;

	if (_fireTimer >= _fireTime) 
	{
		_fireTimer = 0;

		float y = _amplitude * cosf(_frequency * _timer);
		Vec2 spawnPos = Vec2(visibleSize.width, y + screenCenter.y + _margin);
		ArrowPool::getInstance().pop(spawnPos, -Vec2::UNIT_X, _arrowSpeed);

		spawnPos = Vec2(visibleSize.width, y + screenCenter.y - _margin);
		ArrowPool::getInstance().pop(spawnPos, -Vec2::UNIT_X, _arrowSpeed);
	}
}

void DragonPattern::reset()
{
}

bool DragonPattern::isCompleted()
{
	return _timer >= _completeTime;
}
