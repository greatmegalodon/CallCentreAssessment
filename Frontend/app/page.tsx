const HomePage = async () => {
  return (
    <main className="flex flex-col space-y-8 font-sans">
      <div className="w-full lg:w-1/2 mx-auto">
        <h1 className="text-6xl py-6 text-center tracking-tighter font-mono text-transparent bg-clip-text bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500">
          The Finity Technical Challenge
        </h1>
      </div>
      <div className="rounded-md border border-gray-800 px-5 py-8 bg-gradient-to-t from-gray-950 text-gray-300 leading-relaxed">
        <div className="flex flex-col space-y-3 mb-5">
          <p>
            This app is an example that represents our way of working and the
            technologies we use.
          </p>
        </div>
        <ul className="list-disc list-inside ml-1 mb-5">
          <li>Modern React</li>
          <li>Next.js</li>
          <li>Tailwind CSS</li>
          <li>.NET 6+</li>
          <li>Entity Framework Core</li>
          <li>MediatR, supporting a Vertical Sliced architecture</li>
        </ul>
        <div className="flex flex-col space-y-5">
          <p>
            While we sometimes use more or less than this, if you&apos;re
            comfortable with the above you&apos;re sure to be a good fit.
          </p>
          <p>
            The app currently supports viewing and creating Users and Calls.
            Throughout the challenge, it&apos;s important to take in the
            conventions and patterns used in the existing codebase and try to
            adopt them as you make changes; your frontend solution should aim to
            use existing components, for example. We aim to work as a unified
            team, and this is reflected in our code. We want to see your ability
            to work within this environment.
          </p>
          <p>
            If you believe there is a good case for a new piece of tech or a
            package, however, feel free to use it - but also be ready to justify
            it!
          </p>
          <h2 className="text-xl pt-5">Your Task</h2>
          <p>
            Our Stats page, seen in the navigation above, is very empty. Your
            task is to add some stats, detailed below.
          </p>
          <p>
            The imaginary users of this site would love to be able to see
            insights into their usage.
          </p>
          <ul className="list-disc list-inside ml-1 mb-5">
            <li>
              The stats page should only include calls for the current day.
            </li>
            <li>
              Show the number of calls made each hour of the working day, from 9
              AM to 5 PM.
            </li>
            <li>
              Rank the hours based on the number of calls. In the case of a tie,
              give a higher rank to the earlier hour.
            </li>
            <li>
              For each hour of the day, show the user who placed the highest
              number of calls.
            </li>
            <li>
              Display this information in a table. Ensure the table is
              consistent with the current design.
            </li>
          </ul>

          <p>Secondly, display some additional summary stats, including:</p>
          <ul className="list-disc list-inside ml-1 mb-5">
            <li>The date on which the most calls occurred.</li>
            <li>The average number of calls per day.</li>
            <li>The average number of calls per user.</li>
          </ul>
          <p>
            These stats should appear above the table on the stats page. Try to
            align the stats to the current design. Existing components are your
            friend!
          </p>

          <h2 className="text-xl pt-5">Desired Table Output</h2>
          <table>
            <thead className="border border-gray-600 font-bold">
              <tr>
                <td className="p-3">Hour</td>
                <td>Call Count</td>
                <td>Top User</td>
              </tr>
            </thead>
            <tbody>
              <tr className="border border-gray-600">
                <td className="p-3">..</td>
                <td>...</td>
                <td>...</td>
              </tr>
            </tbody>
          </table>
          <p className="italic">
            Note: This table is an example of the data only, not styling or
            code.
          </p>
        </div>
      </div>
    </main>
  );
};

export default HomePage;
