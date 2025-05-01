#pragma once
class TimeCount
{
public:
	void init() { time = 0; }

	void addTime(float _dt)
	{ time += _dt; }

	float getTime() const 
	{ return time; }

private:
	float time = 0;
};

