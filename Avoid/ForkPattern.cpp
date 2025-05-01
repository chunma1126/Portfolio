#include "ForkPattern.h"

ForkPattern::~ForkPattern()
{
}

void ForkPattern::start()
{
	_completeTime = 3;
	_arrowSpeed = 15000;
	_arrowCount = 10;

	auto visibleSize = Director::getInstance()->getVisibleSize();
	Vec2 origin = Director::getInstance()->getVisibleOrigin();

	_startPosX = visibleSize.width + origin.x - _horizontalMargin;
	_endPosX = origin.x + _horizontalMargin;
	float yPos = visibleSize.height + origin.y;

	_arrowPositions.reserve(_arrowCount);

	for (int i = 0; i < _arrowCount; i++) {
		float t = (float)i / (_arrowCount - 1);

		float xPos = _startPosX + t * (_endPosX - _startPosX); 
		_arrowPositions.emplace_back(xPos, yPos);
	}

}

void ForkPattern::update(float dt)
{
	_timer += dt;
	_completeTimer += dt;

	if (_timer >= _arrowInterval && _spawnIndex < _arrowCount)
	{
		_timer = 0;
		ArrowPool::getInstance().pop(_arrowPositions[_spawnIndex++] , -Vec2::UNIT_Y ,_arrowSpeed);
	}

}

void ForkPattern::reset()
{

}

bool ForkPattern::isCompleted()
{
	return _completeTimer >= _completeTime;
}
