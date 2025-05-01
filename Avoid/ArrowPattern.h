#pragma once
#include "ArrowPool.h"

struct ArrowInfo
{
	ArrowInfo() {}
	ArrowInfo(Vec2 _pos, Vec2 _direction, Arrow* _arrow);

	Vec2 pos;
	Vec2 direction;
	Arrow* arrow;
};

class ArrowPattern
{
public:
	virtual ~ArrowPattern() = default;

	virtual void start() = 0;
	virtual	void update(float dt) = 0;
	virtual void reset() = 0;
	virtual bool isCompleted() = 0;
protected:
	int _arrowCount = 0;
	float _timer = 0;
	float _completeTime = 0;
	float _arrowSpeed = 20000.f;

};



