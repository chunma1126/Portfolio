#pragma once
#include "ArrowPattern.h"
class CirclePattern : public ArrowPattern
{
public:
	virtual ~CirclePattern();
	void start() override;
	void update(float dt) override;
	void reset() override;
	bool isCompleted() override;
private:
	float _fireTime = 0.02f;
	int _fireIndex = 1;

	float _completeTimer = 0;
	float _angle = 360;
	float _arrowInteval;
	float _fadeOutTime = 0.45f;

	Vec2 screenCenter;
private:
	Arrow* spawnerArrow;
};

