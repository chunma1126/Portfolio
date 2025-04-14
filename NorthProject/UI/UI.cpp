#include "pch.h"
#include "UI.h"
#include "Texture.h"


UI::UI()
{
		
}
	
UI::~UI()
{

}

void UI::Init()
{

}

void UI::Update()
{
	if (m_active == false)
		return;
	
}

void UI::Render(HDC _hdc)
{
	if (m_active == false)return;

	Vec2 vPos = GetPos();
	Vec2 vSize = GetSize();
	
	int width = m_pTexture->GetWidth();
	int height = m_pTexture->GetHeight();
	
	::TransparentBlt(_hdc
		, (int)(vPos.x - width / 2) 
		, (int)(vPos.y - height / 2)
		, width + vSize.x /2, height + vSize.y/2 ,
		m_pTexture->GetTexDC()
		, 0, 0, width, height, RGB(255, 0, 255));

	
}
