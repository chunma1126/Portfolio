#pragma once
#include "cocos2d.h"
#include <string>
#include <sstream>
#include <iomanip>

class TimeView
{
public:
	void init();
	void setLabel(float _time) 
	{
		std::ostringstream stream;
		stream << std::fixed << std::setprecision(1) << _time;
		timeLabel->setString(stream.str());
	}
	cocos2d::Label* getLabel() { return timeLabel; }
private:
	cocos2d::Label* timeLabel;
};

