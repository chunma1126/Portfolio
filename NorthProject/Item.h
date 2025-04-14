#pragma once
#include "Object.h"
class Texture;
class Item : public Object
{
public:
	Item();
	~Item();
	void Update() override;
	void Render(HDC _hdc) override;
private:
	Texture* m_pTexture;
	
};

