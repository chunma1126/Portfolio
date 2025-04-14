#include "pch.h"
#include "Button.h"
#include "InputManager.h"
#include "Texture.h"
#include "GDISelector.h"
#include "ResourceManager.h";

Button::Button()
{
	GET_SINGLE(ResourceManager)->LoadSound(L"ButtonClick", L"Sound\\ButtonClick.wav", false);
}

Button::~Button()
{

}

RECT Button::GetRect()
{
	int width = m_pTexture->GetWidth();
	int height = m_pTexture->GetHeight();

	Vec2 vPos = { m_vPos.x - width / 2  , m_vPos.y - height / 2 };
	Vec2 vSize = { width + m_vSize.x / 2  , height + m_vSize.y / 2 };

	RECT rt = RECT_MAKE(vPos.x + vSize.x / 2, vPos.y + vSize.y / 2, vSize.x, vSize.y);

	return rt;
}

bool Button::MouseInRect()
{
	POINT mousePos = GET_SINGLE(InputManager)->GetMousePos();
	RECT rt = GetRect();

	return PtInRect(&rt, mousePos);
}

void Button::Update()
{
	UI::Update();

	if (m_active == false)return;

	KEY_STATE state = GET_SINGLE(InputManager)->GetKey(KEY_TYPE::LBUTTON);

	if (MouseInRect())
	{
		if(BUTTON_STATE::DEFAULT == m_state)
			ChangeTex(m_hoverTexture, BUTTON_STATE::HOVER);

		if (state == KEY_STATE::DOWN)
		{
			ChangeTex(m_pressTexture,BUTTON_STATE::CLICK);
		}
		else if (state == KEY_STATE::UP)
		{
			GET_SINGLE(ResourceManager)->PlayAudio(L"ButtonClick");
			ChangeTex(m_originTexture, BUTTON_STATE::DEFAULT);

			ClickEvent();
		}
	}
	else
	{
		ChangeTex(m_originTexture, BUTTON_STATE::DEFAULT);
	}

}

void Button::Render(HDC _hdc)
{
	UI::Render(_hdc);

	int width = m_pTexture->GetWidth();
	int height = m_pTexture->GetHeight();
	
	Vec2 vPos = { m_vPos.x - width / 2  , m_vPos.y - height / 2};
	Vec2 vSize = { width + m_vSize.x / 2  , height + m_vSize.y / 2 };

	//RECT_RENDER(_hdc , vPos.x + vSize.x / 2, vPos.y + vSize.y / 2, vSize.x , vSize.y);

}

void Button::ChangeTex(Texture* _tex,BUTTON_STATE _state)
{
	m_pTexture = _tex;
	m_state = _state;
}