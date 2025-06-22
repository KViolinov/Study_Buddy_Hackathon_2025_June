document.querySelector(".form").addEventListener("submit", async (e) => {
	e.preventDefault()

	const inputText = document.getElementById("textarea").value.trim()
	if (!inputText) {
		alert("Please enter some text.")
		return
	}

	const payload = {
		userId: 1,
		type: "quiz",
		inputSourceType: "normal text",
		inputText: inputText,
	}

	try {
		const postResponse = await fetch(
			"https://study-buddy-hackathon-2025-june.onrender.com/api/inputs",
			{
				method: "POST",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify(payload),
			}
		)

		if (!postResponse.ok) throw new Error("Failed to send input")

		const postData = await postResponse.json()
		const outputText = postData?.data?.output_text || ""

		const quizJsonMatch = outputText.match(/```json\s*([\s\S]*?)\s*```/)
		if (!quizJsonMatch) throw new Error("Could not extract quiz JSON")

		const quizArray = JSON.parse(quizJsonMatch[1])

		const quizDiv = document.createElement("div")
		quizDiv.classList.add("quiz-container")
		let score = 0

		quizDiv.innerHTML = `
			<div class="quiz-section">
				<h2>Generated Quiz</h2>
				${quizArray
					.map(
						(q, idx) => `
						<div class="quiz-question" data-correct="${q.CorrectAnswer}">
							<p><strong>Q${idx + 1}:</strong> ${q.Question}</p>
							<ul>
								${q.Options.map(
									(option) =>
										`<li><label><input type="radio" name="q${idx}" value="${option}"> ${option}</label></li>`
								).join("")}
							</ul>
							<p class="feedback" id="feedback-${idx}"></p>
						</div>
					`
					)
					.join("")}
				<p class="final-score">Score: 0 / ${quizArray.length}</p>
			<div/>
		`

		document.querySelector(".dashboard").appendChild(quizDiv)

		const questions = quizDiv.querySelectorAll(".quiz-question")
		questions.forEach((questionEl, idx) => {
			const correctAnswer = questionEl.dataset.correct
			const radios = questionEl.querySelectorAll("input[type='radio']")
			const feedbackEl = questionEl.querySelector(`#feedback-${idx}`)

			radios.forEach((radio) => {
				radio.addEventListener("change", () => {
					radios.forEach((r) => (r.disabled = true))

					const selected = radio.value
					if (selected === correctAnswer) {
						score++
						feedbackEl.textContent = "✅ Correct!"
						feedbackEl.style.color = "green"
					} else {
						feedbackEl.textContent = `❌ Incorrect. Correct answer: ${correctAnswer}`
						feedbackEl.style.color = "red"
					}

					// Update score display
					quizDiv.querySelector(
						".final-score"
					).textContent = `Score: ${score} / ${quizArray.length}`

					// Highlight correct answer
					radios.forEach((r) => {
						if (r.value === correctAnswer) {
							r.parentElement.style.fontWeight = "bold"
							r.parentElement.style.color = "green"
						}
					})
				})
			})
		})
	} catch (error) {
		console.error("Error:", error.message)
		alert("Something went wrong. See the console for details.")
	}
})
