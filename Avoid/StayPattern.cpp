#include "StayPattern.h"

StayPattern::~StayPattern()
{
	_arrowList.clear();
}

void StayPattern::start()
{
	auto visibleSize = Director::getInstance()->getVisibleSize();
	Vec2 origin = Director::getInstance()->getVisibleOrigin();

	_arrowSpeed = 1000.f;
	_completeTime = 1.9f;
	_arrowCount = 10;

	_arrowList.reserve(_arrowCount);

	int _halfCount = _arrowCount * 0.5f;
	
	for (int i = 0; i < _halfCount; i++)
	{
		Vec2 pos = Vec2(origin.x, (visibleSize.height - _arrowInterval * i) - _margin);
		Vec2 direction = Vec2::UNIT_X;
		Arrow* arrow = (ArrowPool::getInstance().pop(pos, direction, _arrowSpeed));

		_arrowList.emplace_back(pos , direction , arrow);
	}

	for (int i = 0; i < _halfCount; i++)
	{
		Vec2 pos = Vec2(visibleSize.width + origin.x, (visibleSize.height - _arrowInterval * i) - _margin);
		Vec2 direction = -Vec2::UNIT_X;
		Arrow* arrow = ArrowPool::getInstance().pop(pos, direction, _arrowSpeed);

		_arrowList.emplace_back(pos, direction, arrow);
	}

}

void StayPattern::update(float dt)
{

	_completeTimer += dt;
	_timer += dt;

	if (_timer >= _stopTime)
	{
		_stopTime = 999;
		for (auto& arrowInfo : _arrowList)
		{
			arrowInfo.arrow->setDirectionAndSpeed(arrowInfo.direction , _fastArrowSpeed);
		}
	}

}

void StayPattern::reset()
{

}

bool StayPattern::isCompleted()
{
	return _completeTimer >= _completeTime;
}