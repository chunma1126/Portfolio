#pragma once
#include "ArrowPattern.h"
#include <vector>
class StayPattern : public ArrowPattern
{
public:
	virtual ~StayPattern();
	void start() override;
	void update(float dt) override;
	void reset() override;
	bool isCompleted() override;
	
private:
	float _completeTimer = 0;
	float _stopTime = 0.8f;

	float _fastArrowSpeed = 28000;

	int _margin = 55;
	int _arrowInterval = 55;
	std::vector<ArrowInfo> _arrowList;

};