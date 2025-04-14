#pragma once
#include "UI.h"
class Texture;
class Button: public UI
{
public:
	Button();
	~Button();

	RECT GetRect();
	bool MouseInRect();

	void Update() override;
	void Render(HDC _hdc)override;

	void SetPressTexture(Texture* _tex) 
	{
		m_originTexture = m_pTexture;
		m_pressTexture = _tex; 
	}
	void SetHoverTexture(Texture* _tex)
	{
		m_hoverTexture = _tex;
	}

	void ChangeTex(Texture* _tex,BUTTON_STATE _state);

	virtual void ClickEvent() abstract;

protected :
	RECT m_rect;
	BUTTON_STATE m_state;

	Texture* m_hoverTexture;
	Texture* m_pressTexture;
	Texture* m_originTexture;
};

