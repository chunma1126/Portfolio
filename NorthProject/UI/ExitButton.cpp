#include "pch.h"
#include "ExitButton.h"

void ExitButton::ClickEvent()
{
	if (!m_active)return;

	PostQuitMessage(0);
}
