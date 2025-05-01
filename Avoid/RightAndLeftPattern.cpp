#include "RightAndLeftPattern.h"

RightAndLeftPattern::~RightAndLeftPattern()
{
	//CCLOG("delete!!!");
}

void RightAndLeftPattern::start()
{
	_arrowCount = 7;
	_completeTime = 4.1f;

	Vec2 visibleSize = Director::getInstance()->getVisibleSize();
	Vec2 origin = Director::getInstance()->getVisibleOrigin();
	_rightStartPos = visibleSize + origin;
	_leftStartPos = {origin.x , visibleSize.y + origin.x};

	for (int i = 0; i < _arrowCount; i++)
	{
		Vec2 pos = Vec2(_rightStartPos.x, _rightStartPos.y - _arrowInterval * i);
		ArrowPool::getInstance().pop(pos, -Vec2::UNIT_X, _arrowSpeed);
	}
}

void RightAndLeftPattern::update(float dt)
{
	_timer += dt;

	if (_canLeftFire && _timer >= _leftArrowFireTime)
	{
		_canLeftFire = false;

		for (int i = 0; i < _arrowCount; i++)
		{
			Vec2 pos = Vec2(_leftStartPos.x, _leftStartPos.y - _arrowInterval * i);
			ArrowPool::getInstance().pop(pos, Vec2::UNIT_X, _arrowSpeed);
		}
	}
	
}

void RightAndLeftPattern::reset()
{

}

bool RightAndLeftPattern::isCompleted()
{
	return _timer >= _completeTime;
}
