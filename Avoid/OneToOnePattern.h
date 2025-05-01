#pragma once
#include "ArrowPattern.h"
class OneToOnePattern : public ArrowPattern
{
public:
	virtual ~OneToOnePattern();
	void start() override;
	void update(float dt) override;
	void reset() override;
	bool isCompleted() override;
private:
	float _size = 50;
	float _margin = 0;

	float _startPosX = 0;
	float _endPosX = 0;

	float _arrowInterval =0;

	float _completeTimer = 0;
	float _fireTime = 0.1f;
	Vec2 _origin;

	Vec2 _oneShoePos[4];
	float _oneShotSpeed = 25500;
	float _oneShotTime = 0.6f;
	float _oneShotTimer = 0;
	const int _oneShotCount = 4;

};

